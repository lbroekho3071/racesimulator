﻿using System;
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
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        
        private Random _random = new Random(DateTime.Now.Millisecond);
        private Timer _timer;

        public event EventHandler<DriversChangedEventArgs> DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;

            _timer = new Timer(500);
            _timer.Elapsed += OnTimedEvent;

            RandomizeEquipment();
            SetStartingPosition();

            Start();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void SetStartingPosition()
        {
            List<Section> sections =
                Track.Sections.Where((item) => item.SectionType == SectionTypes.StartGrid).Reverse().ToList();

            for (int i = 0; i < Participants.Count; i++)
            {
                if (i / 2 < sections.Count)
                {
                    Section section = sections.ElementAt(i / 2);
                    SectionData data = GetSectionData(section);

                    if (data.Left == null)
                    {
                        data.Left = Participants[i];
                    }
                    else
                    {
                        data.Right = Participants[i];
                    }
                }
            }
        }

        public SectionData GetSectionData(Section section)
        {
            if (_positions.ContainsKey(section)) return _positions[section];

            SectionData data = new SectionData();
            _positions.Add(section, data);

            return data;
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant driver in Participants)
            {
                driver.Equipment.Performance = new Random().Next(1, 100);
                driver.Equipment.Speed = new Random().Next(1, 100);
            }
        }
        
        private void OnTimedEvent(object obj, EventArgs args)
        {
            foreach (IParticipant driver in Participants)
            {
                int speed = driver.Equipment.Performance * driver.Equipment.Speed;
            }
            
            DriversChanged?.Invoke(obj, new DriversChangedEventArgs(Track));
        }
    }
}