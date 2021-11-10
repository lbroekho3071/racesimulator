using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Interfaces;

namespace Model.Classes
{
    public class DataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string TrackName { get; set; }
        public int MaxLaps { get; set; }

        public List<IParticipant> Participants;

        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            TrackName = e.Track.Name;
            MaxLaps = e.MaxLaps;
            Participants = e.Participants;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}