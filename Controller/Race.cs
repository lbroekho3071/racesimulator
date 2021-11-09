using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public int MaxLaps { get; set; }

        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        private Random _random { get; set; }
        private Timer _timer { get; set; }

        public event EventHandler<DriversChangedEventArgs> DriversChanged;
        public event EventHandler RaceFinished;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            MaxLaps = 1;

            _random = new Random(DateTime.Now.Millisecond);

            _timer = new Timer(500);
            _timer.Elapsed += OnTimedEvent;

            RandomizeEquipment();
            SetStartingPositions();
        }

        public SectionData GetSectionData(Section section)
        {
            if (_positions.ContainsKey(section)) return _positions[section];

            var data = new SectionData();
            _positions.Add(section, data);

            return data;
        }

        public void Start()
        {
            _timer.Start();
        }

        private void OnTimedEvent(object obj, EventArgs args)
        {
            if (Participants.Count(item => item.Laps > MaxLaps) == Participants.Count)
            {
                for (int i = 0; i < Participants.Count; i++)
                {
                    Participants[i].Laps = 0;
                }

                DriversChanged = null;
                RaceFinished?.Invoke(this, new EventArgs());
            }
            
            MoveParticipants();

            DriversChanged?.Invoke(this, new DriversChangedEventArgs
            {
                Track = Track
            });
        }

        private void SetStartingPositions()
        {
            List<Section> sections = Track.Sections.Where(item => item.SectionType == SectionTypes.StartGrid).ToList();

            for (int i = 0; i < Participants.Count; i++)
            {
                if (i / 2 < sections.Count)
                {
                    SectionData data = GetSectionData(sections.ElementAt(i / 2));

                    if (data.Left == null)
                    {
                        data.DistanceLeft = 60;
                        data.Left = Participants[i];
                    }
                    else if (data.Right == null)
                    {
                        data.DistanceRight = 40;
                        data.Right = Participants[i];
                    }
                }
            }
        }

        private void MoveParticipants()
        {
            Console.WriteLine("test");
            foreach (IParticipant participant in Participants)
            {
                BreakDownCar(participant);
                
                if (participant.Laps > MaxLaps)
                    continue;

                if (participant.Equipment.IsBroken)
                    continue;

                Section section = _positions
                    .SingleOrDefault(item =>
                        item.Value.Left == participant || item.Value.Right == participant).Key;
                

                if (section != null)
                {
                    SectionData sectionData = GetSectionData(section);

                    int speed = participant.Equipment.Performance * participant.Equipment.Speed;

                    if (sectionData.Left == participant)
                    {
                        if (speed + sectionData.DistanceLeft > 100)
                        {
                            if (MoveParticipantNextGrid(section, participant, speed + sectionData.DistanceLeft))
                            {
                                sectionData.Left = null;
                                sectionData.DistanceLeft = 0;
                            }
                        }
                        else
                        {
                            sectionData.DistanceLeft += speed;
                        }
                    }
                    else if (sectionData.Right == participant)
                    {
                        if (speed + sectionData.DistanceRight > 100)
                        {
                            if (MoveParticipantNextGrid(section, participant, speed + sectionData.DistanceRight))
                            {
                                sectionData.Right = null;
                                sectionData.DistanceRight = 0;
                            }
                        }
                        else
                        {
                            sectionData.DistanceRight += speed;
                        }
                    }
                }
            }
        }

        private bool MoveParticipantNextGrid(Section section, IParticipant participant, int speed)
        {
            (bool finish, SectionData sectionData) = GetNextSectionData(section, speed);

            if (finish && !participant.Equipment.IsBroken)
                participant.Laps += 1;
            
            if (sectionData.Left == null)
            {
                if (participant.Laps <= MaxLaps)
                {
                    sectionData.Left = participant;
                    sectionData.DistanceLeft = speed - speed / 100 * 100;
                }

                return true;
            }
            if (sectionData.Right == null)
            {
                if (participant.Laps <= MaxLaps)
                {
                    sectionData.Right = participant;
                    sectionData.DistanceRight = speed - speed / 100 * 100;
                }

                return true;
            }

            return false;
        }

        private void RandomizeEquipment()
        {
            foreach (IParticipant driver in Participants)
            {
                driver.Equipment.Performance = _random.Next(15, 20);
                driver.Equipment.Speed = _random.Next(15, 20);
            }
        }
        
        private void BreakDownCar(IParticipant participant)
        {
            int number = _random.Next(100);
        
            IEquipment equipment = participant.Equipment;
            if (number <= 2 )
            {
                equipment.IsBroken = true;
                equipment.Performance -= 1;
                equipment.Speed -= 1;
            }
            else
            {
                if (number > 10) 
                    equipment.IsBroken = false;
            }
        }

        private (bool, SectionData) GetNextSectionData(Section section, int position)
        {
            List<Section> tracks = Track.Sections.ToList();

            int index = tracks.IndexOf(section) + position / 100;

            if (index < Track.Sections.Count)
            {
                int lastIndex = index - position / 100;

                return (tracks.GetRange(lastIndex, index - lastIndex)
                    .Exists(item => item.SectionType == SectionTypes.Finish), GetSectionData(tracks.ElementAt(index)));
            }

            return (false, GetSectionData(tracks.ElementAt(index - Track.Sections.Count)));
        }
    }
}