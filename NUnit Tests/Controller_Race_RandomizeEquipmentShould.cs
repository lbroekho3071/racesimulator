using Controller;
using Model.Interfaces;
using NUnit.Framework;

namespace NUnit_Tests
{
    public class Controller_Race_RandomizeEquipmentShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
        }

        [Test]
        public void RandomizeEquipment_ParticipantsEquipment_AreSet()
        {
            foreach (IParticipant participant in Data.CurrentRace.Participants)
            {
                Assert.AreNotEqual(default(int), participant.Equipment.Performance);
                Assert.AreNotEqual(default(int), participant.Equipment.Speed);
            }
        }
    }
}