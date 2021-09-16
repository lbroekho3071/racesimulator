using System;
using System.Collections.Generic;
using Model.Classes;
using Model.Enums;

namespace Simulator
{
    public static class Visualization
    {
        #region graphics
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
                string[] stringArray = VisualEnums[section.SectionType];
                foreach (string item in stringArray)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}