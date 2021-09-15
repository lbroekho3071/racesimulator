using Model.Classes;
using Model.Enums;
using NUnit.Framework;

namespace NUnit_Test
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_FilledQueue_ReturnTrack()
        {
            Track track = new Track("Test", new[] {SectionTypes.StartGrid});
            _competition.Tracks.Enqueue(track);

            Track result = _competition.NextTrack();
            
            Assert.AreEqual(track, result);
        }
        
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track track = new Track("Test", new[] {SectionTypes.StartGrid});
            _competition.Tracks.Enqueue(track);

            _competition.NextTrack();
            Track result = _competition.NextTrack();
            
            Assert.IsNull(result);
        }
        
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            _competition.Tracks.Enqueue(new Track("Test", new[] {SectionTypes.StartGrid}));
            Track track = new Track("feliz navidad", new[] {SectionTypes.Finish});
            _competition.Tracks.Enqueue(track);

            _competition.NextTrack();
            Track result = _competition.NextTrack();
            
            Assert.AreEqual(track, result);
        }
    }
}