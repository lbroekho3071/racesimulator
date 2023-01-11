using System.Linq;
using Controller;
using Model.Classes;
using Model.Enums;
using NUnit.Framework;

namespace NUnit_Tests
{
    [TestFixture]
    public class Controller_Race_GetSectionDataShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
            Data.NextRace();
        }

        [Test]
        public void GetSectionData_ExistingSection_ReturnSectionData()
        {
            Section section = Data.CurrentRace.Track.Sections.ElementAt(1);
            
            Assert.IsTrue(Data.CurrentRace.HasSectionData(section));
            
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            
            Assert.IsNotNull(sectionData);
        }

        [Test]
        public void GetSectionData_NonExistingSection_ReturnSectionData()
        {
            Section section = Data.CurrentRace.Track.Sections.ElementAt(5);
            
            Assert.IsFalse(Data.CurrentRace.HasSectionData(section));

            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            
            Assert.IsNotNull(sectionData);
            Assert.IsTrue(Data.CurrentRace.HasSectionData(section));
        }

        [Test]
        public void GetSectionData_NullSection_ReturnNull()
        {
            SectionData sectionData = Data.CurrentRace.GetSectionData(null);
            
            Assert.IsNull(sectionData);
        }
    }
}