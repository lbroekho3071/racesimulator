using System;
using System.Windows;
using Controller;
using Model.Classes;

namespace WPF
{
    public partial class RaceWindow : Window
    {
        public RaceWindow()
        {
            InitializeComponent();

            Data.CurrentRace.RaceFinished += OnRaceFinish;
            
            Dispatcher.Invoke(() =>
            {
                Data.CurrentRace.DriversChanged += ((DataContext) this.DataContext).OnDriversChanged;
            });
        }

        public void OnRaceFinish(object sender, EventArgs args)
        {
            if (Data.CurrentRace != null)
            {
                Dispatcher.Invoke(() =>
                {
                    Data.CurrentRace.DriversChanged += ((DataContext) this.DataContext).OnDriversChanged;
                });
            }
        }
    }
}