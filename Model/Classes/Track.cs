using System;
using System.Collections.Generic;
using Model.Enums;

namespace Model.Classes
{
    public class Track
    {
        public string Name;
        public LinkedList<Section> Sections;

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = ConvertArrayToLinkedList(sections);
        }
        
        public LinkedList<Section> ConvertArrayToLinkedList(SectionTypes[] sections)
        {
            LinkedList<Section> sectionList = new LinkedList<Section>();

            foreach (SectionTypes section in sections)
            {
                sectionList.AddLast(new Section(section));
            }

            return sectionList;
        }
    }
}