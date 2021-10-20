using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Model.Classes;
using Model.Enums;
using Model.Interfaces;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        public bool RaceFinished = false;
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        
        private Random _random = new Random(DateTime.Now.Millisecond);
        private Timer _timer;
        
        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            
            RandomizeEquipment();
            SetStartingPosition();

            _timer = new Timer(500);
            _timer.Elapsed += OnTimedEvent;

            Start();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void SetStartingPosition()
        {
            var sections =
                Track.Sections.Where((item) => item.SectionType == SectionTypes.StartGrid).Reverse().ToList();

            for (var i = 0; i < Participants.Count; i++)
                if (i / 2 < sections.Count)
                {
                    var section = sections.ElementAt(i / 2);
                    var data = GetSectionData(section);

                    if (data.Left == null)
                    {
                        data.DistanceLeft = 40;
                        data.Left = Participants[i];
                    }
                    else
                    {
                        data.DistanceRight = 60;
                        data.Right = Participants[i];
                    }
                }
        }
        

        public SectionData GetSectionData(Section section)
        {
            if (_positions.ContainsKey(section)) return _positions[section];

            var data = new SectionData();
            _positions.Add(section, data);

            return data;
        }

        public void RandomizeEquipment()
        {
            foreach (var driver in Participants)
            {
                driver.Equipment.Performance = new Random().Next(1, 100);
                driver.Equipment.Speed = new Random().Next(1, 100);
            }
        }
        
        private void OnTimedEvent(object obj, EventArgs args)
        {
            MoveParticipants();
            
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track));
        }

        private void MoveParticipants()
        {
            foreach (var participant in Participants)
            {
                Section section = _positions.SingleOrDefault(
                    item => item.Value.Left == participant || item.Value.Right == participant).Key;

                if (section != null)
                {
                    var sectionData = GetSectionData(section);
                    
                    var position = participant.Equipment.Performance * participant.Equipment.Speed + sectionData.DistanceLeft;
                    
                    if (participant == sectionData.Left)
                    {
                        if (position > 100)
                        {
                            NextSection(section, participant, position);
                    
                            sectionData.Left = null;
                            sectionData.DistanceLeft = 0;
                        }
                        else
                        {
                            sectionData.DistanceLeft = position;
                        }
                    }
                    else if (participant == sectionData.Right)
                    {
                        if (position > 100)
                        {
                            NextSection(section, participant, position);
                    
                            sectionData.Right = null;
                            sectionData.DistanceRight = 0;
                        }
                        else
                        {
                            sectionData.DistanceRight = position;
                        }
                    }
                }
            }
        }

        private void NextSection(Section section, IParticipant participant, int position)
        {
            SectionData nextData = GetNextSection(section);

            if (section.SectionType == SectionTypes.Finish)
            {
                participant.Laps += 1;
            }

            if (nextData.Left == null)
            {
                nextData.Left = participant;
                nextData.DistanceLeft = position - 100;
            }
            else if (nextData.Right == null)
            {
                nextData.Right = participant;
                nextData.DistanceRight = position - 100;
            }
        }

        private SectionData GetNextSection(Section section)
        {
            int index = Track.Sections.ToList().IndexOf(section) + 1;

            if (index < Track.Sections.Count) 
                return GetSectionData(Track.Sections.ElementAt(index));

            return GetSectionData(Track.Sections.ElementAt(0));
            
        }
    }
}