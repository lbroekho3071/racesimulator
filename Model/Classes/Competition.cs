using System.Collections.Generic;
using Model.Interfaces;

namespace Model.Classes
{
    public class Competition
    {
        public List<IParticipant> Participants = new List<IParticipant>();
        public Queue<Track> Tracks = new Queue<Track>();

        public Track NextTrack()
        {
            if (Tracks.Count == 0) return null;

            return Tracks.Dequeue();
        }
    }
}