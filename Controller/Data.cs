using System;
using Model.Classes;
using Model.Enums;

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
                Competition.Participants.Add(new Driver());
            }
        }

        private static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("Test", new []
            {
                SectionTypes.StartGrid, 
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish,
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