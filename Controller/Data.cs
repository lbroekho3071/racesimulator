using System;
using Model.Classes;
using Model.Enums;
using Model.Interfaces;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            Competition = new Competition();
            
            AddParticipants();
            AddTracks();
        }

        private static void AddParticipants()
        {
            foreach (TeamColors color in Enum.GetValues(typeof(TeamColors)))
            {
                Competition.Participants.Add(new Driver(color.ToString(), new Car(), color));
            }
        }

        private static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("Test", new []
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
            }));
        }

        public static void NextRace()
        {
            Track track = Competition.NextTrack();

            if (track == null) return;

            CurrentRace = new Race(track, Competition.Participants);
        }
    }
}