using System.Collections.Generic;
using Model.Interfaces;

namespace Model.Classes
{
    public class Competition
    {
        public List<IParticipant> Participants;
        public Queue<Track> Tracks;

        // public Track NextTrack()
        // {
        //     Tracks.
        // }
    }
}