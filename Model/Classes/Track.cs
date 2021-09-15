using System;
using System.Collections.Generic;
using Model.Enums;

namespace Model.Classes
{
    public class Track
    {
        public string Name;
        public LinkedList<Section> Sections;

        public Track(string name, Section[] sections)
        {
            Name = name;
            foreach (Section section in sections)
            {
                Sections.AddLast(section);
            }
        }
    }
}