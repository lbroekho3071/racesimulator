using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Controller;
using Model.Interfaces;

namespace Model.Classes
{
    public class DataContext : INotifyPropertyChanged
    {
        public string TrackName => Data.CurrentRace.Track.Name;
        public List<Driver> Participants => Data.Competition.Participants.Select(p => (Driver)p).ToList();

        public event PropertyChangedEventHandler PropertyChanged;

        public DataContext()
        {
            if (Data.CurrentRace != null)
                Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}