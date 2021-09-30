using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using Model.Classes;
using Model.Enums;

namespace Simulator
{
    public static class Visualization
    {
        #region graphics

        public static List<List<Section>> Sections = new List<List<Section>>()
        {
            new List<Section>() {null, null, null, new Section(SectionTypes.Finish), new Section(SectionTypes.StartGrid)}
        };

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
            Direction direction = Direction.East;
            Section sectionValue;
            Point point = new Point(4, 0);
            
            foreach (Section section in track.Sections)
            {
                point = GetSectionPosition(direction, point);
                try
                {
                    sectionValue = Sections[point.Y][point.X];
                }
                catch(ArgumentOutOfRangeException e)
                {
                    Sections[point.Y].Add(null);
                    sectionValue = Sections[point.Y][point.X];
                }
                
                Console.WriteLine(sectionValue);
            }
            Console.WriteLine(Sections[0]);
        }

        private static Point GetSectionPosition(Direction direction, Point point)
        {
            switch(direction)
            {
                case Direction.North:
                    point.Y--;
                    break;
                case Direction.East:
                    point.X++;
                    break;
                case Direction.South:
                    point.Y++;
                    break;
                case Direction.West:
                    point.X--;
                    break;
            }

            return point;
        }
    }
}