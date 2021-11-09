using System;
using System.Collections.Generic;
using Model.Interfaces;

namespace Model.Classes
{
    public class DriversChangedEventArgs : EventArgs
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
    }
}