using System;
using System.Collections.Generic;
using Model.Classes;
using Model.Interfaces;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random = new Random(DateTime.Now.Millisecond);
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
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
    }
}