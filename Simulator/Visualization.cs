using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Model.Classes;
using Model.Enums;

namespace Simulator
{
    public static class Visualization
    {
        #region graphics
        
        public static Point Position = new Point(20, 0);
        public static int Direction = 1;

        public static Dictionary<SectionTypes, string[]> VisualEnums = new Dictionary<SectionTypes, string[]>();
        
        public static void Initialize()
        {
            VisualEnums.Add(SectionTypes.StartGrid, new []{ "----", "  # ", "  # ", "----" });
            VisualEnums.Add(SectionTypes.Straight, new []{"----", "    ", "    ", "----"});
            VisualEnums.Add(SectionTypes.LeftCorner, new []{"/  |", "   |", "   /", "--/ "});
            VisualEnums.Add(SectionTypes.RightCorner, new []{"--\\ ", "   \\", "   |", "\\  |"});
            VisualEnums.Add(SectionTypes.Finish, new []{"--|-", "  | ", "  | ", "--|-"});
        }
        #endregion

        public static void DrawTrack(Track track)
        {
            foreach (Section section in track.Sections)
            {
                string[] visuals = VisualEnums[section.SectionType];
                
                Direction = GetDirection(section);
                SetSectionPosition(section);

                for (int i = 0; i < visuals.Length; i++)
                {
                    Console.SetCursorPosition(section.Position.X, section.Position.Y + i);
                    Console.WriteLine(visuals[i]);
                }
            }
        }

        private static int GetDirection(Section section)
        {
            SectionTypes type = section.SectionType;

            switch (type)
            {
                case SectionTypes.LeftCorner:
                    return Clamp(Direction - 1, 0, 3);
                case SectionTypes.RightCorner:
                    return Clamp(Direction + 1, 0, 3);
                default:
                    return Direction;
            }
        }

        private static void SetSectionPosition(Section section)
        {
            section.Position = Position;

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
        
        private static void SetSectionVisual(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.StartGrid:
                    switch (section.Direction)
                    {
                        case 0: case 2:
                            section.VisualTrack = StartGridVertical;
                            break;
                        default:
                            section.VisualTrack = StartGridHortizontal;
                            break;
                    }
                    break;
                
                case SectionTypes.Straight:
                    switch (section.Direction)
                    {
                        case 0: case 2:
                            section.VisualTrack = StraightVertical;
                            break;
                        default:
                            section.VisualTrack = StraightHorizontal;
                            break;
                    }
                    break;

                case SectionTypes.LeftCorner:
                    switch (section.Direction)
                    {
                        case 0:
                            section.VisualTrack = CornerNorthWest;
                            break;
                        case 1:
                            section.VisualTrack = CornerNorthEast;
                            break;
                        case 2:
                            section.VisualTrack = CornerSouthEast;
                            break;
                        case 3:
                            section.VisualTrack = CornerSouthWest;
                            break;
                    }
                    break;
                
                case SectionTypes.RightCorner:
                    switch (section.Direction)
                    {
                        case 3:
                            section.VisualTrack = CornerNorthWest;
                            break;
                        case 2:
                            section.VisualTrack = CornerSouthWest;
                            break;
                        case 1:
                            section.VisualTrack = CornerNorthEast;
                            break;
                        case 0:
                            section.VisualTrack = CornerSouthEast;
                            break;
                    }
                    break;
                
                case SectionTypes.Finish:
                    switch (section.Direction)
                    {
                        case 0: case 2:
                            section.VisualTrack = FinishVertical;
                            break;
                        case 1: case 3:
                            section.VisualTrack = FinishHorizontal;
                            break;
                    }
                    break;
            }
        }
        
        private static int Clamp( int value, int min, int max )
        {
            return (value < min) ? max : (value > max) ? min : value;
        }
    }
}