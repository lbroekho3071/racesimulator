using System;

namespace Model.Classes
{
    public class DriversChangedEventArgs : EventArgs
    {
        public Track Track { get; set; }
    }
}