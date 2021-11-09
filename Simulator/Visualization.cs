using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Controller;
using Model.Classes;
using Model.Enums;
using Model.Interfaces;

namespace Simulator
{
    public static class Visualization
    {
        public static Point Position = new Point(20, 0);
        public static int Direction { get; set; }

        #region graphics
        public static string[] StraightHorizontal = new[] {"----", "  1 ", " 2  ", "----"};
        public static string[] StraightVertical = new[] {"|  |", "|1 |", "| 2|", "|  |"};

        public static string[] CornerNorthWest = new[] {"/  |", " 1 |", "  2/", "--/ "};
        public static string[] CornerNorthEast = new[] {" /--", "/1  ", "| 2 ", "|  /"};
        public static string[] CornerSouthWest = new[] {"--\\ ", "  1\\", " 2 |", "\\  |"};
        public static string[] CornerSouthEast = new[] {"|  \\", "| 1 ", "\\2  ", " \\-- "};

        public static string[] StartGridHortizontal = new[] {"----", "  1#", "2#  ", "----"};
        public static string[] StartGridVertical = new[] {"|  |", "|#1|", "|2#|", "|  |"};
        
        public static string[] FinishHorizontal = new[] {"--|-", " 1| ", " 2| ", "--|-"};
        public static string[] FinishVertical = new[] {"| 2|", "|1 |", "----", "|  |"};
        #endregion

        public static void Initialize()
        {
            Direction = 1;

            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;
        }

        public static void DrawTrack(Track track)
        {
            // Console.Clear();
            
            foreach (Section section in track.Sections)
            {
                SetPosition();
                SetDirection(section);

                string[] visuals = GetSectionVisual(section);
                SectionData sectionData = Data.CurrentRace.GetSectionData(section);

                for (int i = 0; i < visuals.Length; i++)
                {
                    Console.SetCursorPosition(Position.X, Position.Y + i);
                    Console.Write(DrawParticipant(visuals[i], sectionData));
                }
            }
        }

        private static string DrawParticipant(string visual, SectionData data)
        {
            IParticipant p1 = data.Left;
            IParticipant p2 = data.Right;

            visual = visual.Replace("1", p1 == null ? " " : p1.Equipment.IsBroken ? "~" : p1.Name.Substring(0, 1));

            visual = visual.Replace("2", p2 == null ? " " : p2.Equipment.IsBroken ? "~" : p2.Name.Substring(0, 1));

            return visual;
        }

        private static string[] GetSectionVisual(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.StartGrid:
                    switch (Direction)
                    {
                        case 0: case 2:
                            return StartGridVertical;
                        default:
                            return StartGridHortizontal;
                    }
        
                case SectionTypes.Straight:
                    switch (Direction)
                    {
                        case 0: case 2:
                            return StraightVertical;
                        default:
                            return StraightHorizontal;
                    }
        
                case SectionTypes.LeftCorner:
                    switch (Direction)
                    {
                        case 0:
                            return CornerNorthWest;
                        case 1:
                            return CornerSouthEast;
                        case 2:
                            return CornerNorthEast;
                        default:
                            return CornerSouthWest;
                    }
                
                case SectionTypes.RightCorner:
                    switch (Direction)
                    {
                        case 3:
                            return CornerNorthWest;
                        case 2:
                            return CornerSouthWest;
                        case 1:
                            return CornerNorthEast;
                        default:
                            return CornerSouthEast;
                    }
                default:
                    switch (Direction)
                    {
                        case 0: case 2:
                            return FinishVertical;
                        default:
                            return FinishHorizontal;
                    }
            }
        }

        private static void SetPosition()
        {
            switch (Direction)
            {
                case 0:
                    Position.Y -= 4;
                    break;
                case 1:
                    Position.X += 4;
                    break;
                case 2:
                    Position.Y += 4;
                    break;
                case 3:
                    Position.X -= 4;
                    break;
            }
        }

        private static void SetDirection(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.LeftCorner:
                    Direction = Clamp(Direction - 1, 0, 3);
                    break;
                case SectionTypes.RightCorner:
                    Direction = Clamp(Direction + 1, 0, 3);
                    break;
            }
        }

        private static int Clamp( int value, int min, int max )
        {
            return (value < min) ? max : (value > max) ? min : value;
        }
        
        
        
        public static void OnDriversChanged(object obj, DriversChangedEventArgs args)
        {
            DrawTrack(args.Track);
        }

        public static void OnRaceFinished(object obj, EventArgs args)
        {
            Console.Clear();
            
            if (Data.CurrentRace != null)
            {
                if (Data.Competition.Tracks.Count > 0)
                {
                    Data.NextRace();
                
                    Initialize();
                    DrawTrack(Data.CurrentRace.Track);
                }
            }
        }
    }
}