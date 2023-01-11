using System.Collections.Generic;
using System.Linq;
using Controller;
using Model.Classes;
using Model.Enums;
using NUnit.Framework;

namespace NUnit_Tests
{
    public class Controller_Race_GetIndexOfSectionShould
    {
        private Section _section;
        
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
            
            _section = Data.CurrentRace.Track.Sections.ToList().ElementAt(10);
        }

        [Test]
        public void GetIndexOfSection_PlayerNotMovedFromSection_ReturnInt()
        {
            int index = Data.CurrentRace.GetIndexOfSection(_section, 20);

            Assert.AreEqual(10, index);
        }

        [Test]
        public void GetIndexOfSection_PlayerMovedOneSection_ReturnInt()
        {
            int index = Data.CurrentRace.GetIndexOfSection(_section, 120);
            
            Assert.AreEqual(11, index);
        }
        
        [Test]
        public void GetIndexOfSection_PlayerMovedTwoSections_ReturnInt()
        {
            int index = Data.CurrentRace.GetIndexOfSection(_section, 220);
            
            Assert.AreEqual(12, index);
        }

        [Test]
        public void GetIndexOfSection_LastExisitingSection_ReturnInt()
        {
            Section section = Data.CurrentRace.Track.Sections.ToList().Last();
            int index = Data.CurrentRace.GetIndexOfSection(section, 110);
            
            Assert.AreEqual(0, index);
        }
    }
}