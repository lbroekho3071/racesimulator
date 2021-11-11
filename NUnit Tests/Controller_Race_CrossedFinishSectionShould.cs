using System.Collections.Generic;
using System.Linq;
using Controller;
using Model.Classes;
using Model.Enums;
using NUnit.Framework;

namespace NUnit_Tests
{
    [TestFixture]
    public class Controller_Race_CrossedFinishSectionShould
    {
        private int _finishIndex;
        
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();

            List<Section> sections = Data.CurrentRace.Track.Sections.ToList();

            _finishIndex = sections.FindIndex(item => item.SectionType == SectionTypes.Finish);
        }

        [Test]
        public void CrossedFinishSection_ParticipantCrossedFinish_ReturnTrue()
        {
            bool finished = Data.CurrentRace.CrossedFinishSection(_finishIndex + 2, 1);
            
            // Assert.IsNull(_finishIndex);
            Assert.IsTrue(finished);
        }
    }
}