using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Model.Classes;
using Model.Enums;
using NUnit.Framework;

namespace NUnit_Tests
{
    [TestFixture]
    public class Controller_Race_SetStartingPositionsShould
    {
        private LinkedList<Section> _sections;
        
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
            _sections = Data.CurrentRace.Track.Sections;
        }

        [Test]
        public void SetStartingPositions_TotalParticipantsSet_EqualToStartPositions()
        {
            int count = 0;
            int startGridCount = _sections.Count(item => item.SectionType == SectionTypes.StartGrid) * 2;
            int participantsCount = Enum.GetNames(typeof(TeamColors)).Length;

            foreach (Section section in _sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    SectionData sectionData = Data.CurrentRace.GetSectionData(section);

                    if (sectionData.Left != null)
                        count += 1;

                    if (sectionData.Right != null)
                        count += 1;
                }
            }
            
            Assert.AreEqual(startGridCount >= participantsCount ? participantsCount : startGridCount, count);
        }

        [Test]
        public void SetStartingPositions_ParticipantsNotSetInOtherGrid_EqualToZero()
        {
            int count = 0;
            
            foreach (Section section in _sections)
            {
                if (section.SectionType != SectionTypes.StartGrid)
                {
                    SectionData sectionData = Data.CurrentRace.GetSectionData(section);

                    if (sectionData.Left != null)
                        count += 1;

                    if (sectionData.Right != null)
                        count += 1;
                }
            }
            
            Assert.AreEqual(0, count);
        }
    }
}