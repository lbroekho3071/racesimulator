using System;
using Controller;
using NUnit.Framework;

namespace NUnit_Tests
{
    public class Controller_Data_NextRaceShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
        }

        [Test]
        public void NextRace_FilledSet_ReturnTrack()
        {
            Assert.IsNotNull(Data.CurrentRace);
        }

        [Test]
        public void NextRace_EmptySet_ReturnNull()
        {
            Data.NextRace();

            Assert.IsNull(Data.CurrentRace);
        }
    }
}