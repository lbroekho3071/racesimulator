﻿using System;

namespace Model.Classes
{
    public class DriversChangedEventArgs : EventArgs
    {
        public Track Track { get; set; }

        public DriversChangedEventArgs(Track track)
        {
            this.Track = track;
        }
    }
}