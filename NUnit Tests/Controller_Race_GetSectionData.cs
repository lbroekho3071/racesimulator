using Controller;
using NUnit.Framework;

namespace NUnit_Tests
{
    [TestFixture]
    public class Controller_Race_GetSectionData
    {
        private Race _race;

        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
            
            _race = Data.CurrentRace;
        }

        [Test]
        public void GetSectionData_SectionOutOfRange_ReturnNull()
        {
            
        }
    }
}