using System.Drawing;
using Model.Enums;

namespace Model.Classes
{
    public class Section
    {
        public SectionTypes SectionType;
        public Point Position { get; set; }
        public int Direction { get; set; }

        public Section(SectionTypes sectionType)
        {
            SectionType = sectionType;
        }
    }
}