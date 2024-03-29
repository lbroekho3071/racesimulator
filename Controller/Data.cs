﻿using System;
using System.Collections.Generic;
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

        public static void AddParticipants()
        {
            foreach (TeamColors color in Enum.GetValues(typeof(TeamColors)))
            {
                Competition.Participants.Add(new Driver
                {
                    Name = color.ToString(),
                    Equipment = new Car(),
                    TeamColor = color
                });
            }
        }

        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("Test", new []
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
            }));
            
            Competition.Tracks.Enqueue(new Track("kleiner rondje", new []
            {
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.StartGrid,
                SectionTypes.Finish,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
            }));
        }

        public static void NextRace()
        {
            Track track = Competition.NextTrack();

            if (track == null)
            {
                CurrentRace = null;
                return;
            }

            CurrentRace = new Race(track, Competition.Participants);
            
            CurrentRace.Start();
        }
    }
}