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
    }
}