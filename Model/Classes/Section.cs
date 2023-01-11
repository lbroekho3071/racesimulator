using System.Drawing;
using Model.Enums;

namespace Model.Classes
{
    public class Section
    {
        public SectionTypes SectionType;

        public Section(SectionTypes sectionType)
        {
            SectionType = sectionType;
        }
    }
}