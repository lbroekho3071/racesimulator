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
            SectionData sectionData = Data.CurrentRace.GetSectionData(Data.CurrentRace.Track.Sections.ElementAt(1));
            
            Assert.IsNotNull(sectionData);
        }
    }
}