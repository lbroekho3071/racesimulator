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
        public static Track Track;
        public static int Direction = 1;
        
        #region graphics
        public static string[] StraightHorizontal = new[] {"----", "  1 ", " 2  ", "----"};
        public static string[] StraightVertical = new[] {"|  |", "|1 |", "| 2|", "|  |"};

        public static string[] CornerNorthWest = new[] {"/  |", " 1 |", "  2/", "--/ "};
        public static string[] CornerNorthEast = new[] {" /--", "/1  ", "| 2 ", "|  /"};
        public static string[] CornerSouthWest = new[] {"--\\ ", "  1\\", " 2 |", "\\  |"};
        public static string[] CornerSouthEast = new[] {"|  \\", "| 1 ", "\\2  ", " \\-- "};

        public static string[] StartGridHortizontal = new[] {"----", "  1#", "2#  ", "----"};
        public static string[] StartGridVertical = new[] {"|  |", "|# |", "| #|", "|  |"};
        
        public static string[] FinishHorizontal = new[] {"--|-", " 1| ", " 2| ", "--|-"};
        public static string[] FinishVertical = new[] {"|  |", "|  |", "----", "|  |"};
        #endregion

        public static void Initialize(Track track)
        {
            Track = track;
        }

        public static void DrawTrack()
        {
            foreach (Section section in Track.Sections)
            {
                SetDirection(section);
                SetSectionPosition(section);
                string[] visuals = GetSectionVisual(section);
                
                for (int i = 0; i < visuals.Length; i++)
                {
                    Console.SetCursorPosition(section.Position.X, section.Position.Y + i);
                    Console.WriteLine(DrawParticipants(visuals[i], Data.CurrentRace.GetSectionData(section)));
                }
            }

        }

        private static string DrawParticipants(string visual, SectionData data)
        {
            IParticipant p1 = data.Left;
            IParticipant p2 = data.Right;

            visual = visual.Replace("1", p1 == null ? " " : p1.Name.Substring(0, 1));
            visual = visual.Replace("2", p2 == null ? " " : p2.Name.Substring(0, 1));
            
            return visual;
        }
        
        private static void SetDirection(Section section)
        {
            SectionTypes type = section.SectionType;

            switch (type)
            {
                case SectionTypes.LeftCorner:
                    Direction = Clamp(Direction - 1, 0, 3);
                    break;
                case SectionTypes.RightCorner:
                    Direction = Clamp(Direction + 1, 0, 3);
                    break;
            }
        }

        private static void SetSectionPosition(Section section)
        {
            section.Position = Position;
            section.Direction = Direction;

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
        
        private static string[] GetSectionVisual(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.StartGrid:
                    switch (section.Direction)
                    {
                        case 0: case 2:
                            return StartGridVertical;
                        default:
                            return StartGridHortizontal;
                    }

                case SectionTypes.Straight:
                    switch (section.Direction)
                    {
                        case 0: case 2:
                            return StraightVertical;
                        default:
                            return StraightHorizontal;
                    }

                case SectionTypes.LeftCorner:
                    switch (section.Direction)
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
                    switch (section.Direction)
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
                    switch (section.Direction)
                    {
                        case 0: case 2:
                            return FinishVertical;
                        default:
                            return FinishHorizontal;
                    }
            }
        }
        
        private static int Clamp( int value, int min, int max )
        {
            return (value < min) ? max : (value > max) ? min : value;
        }
    }
}