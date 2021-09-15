using System;
using System.Collections.Generic;
using Model.Enums;

namespace Model.Classes
{
    public class Track
    {
        public string Name;
        public LinkedList<Section> Sections = new LinkedList<Section>();

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            foreach (SectionTypes section in sections)
            {
                Sections.AddLast(new Section(section));
            }
        }
    }
}