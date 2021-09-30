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

        public static int PositionX = 20;
        public static int PositionY;
        
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

                foreach (string visual in visuals)
                {
                    Console.SetCursorPosition(PositionX, PositionY);
                    Console.WriteLine(visual);
                
                    PositionY++;
                }
                
                PositionY -= 4;
                PositionX += 4;
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
        
        private static int Clamp( int value, int min, int max )
        {
            return (value < min) ? max : (value > max) ? min : value;
        }
    }
}