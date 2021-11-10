using System;
using System.Windows;
using Controller;
using Model.Classes;

namespace WPF
{
    public partial class CompWindow : Window
    {
        public CompWindow()
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
            Dispatcher.Invoke(() =>
            {
                Data.CurrentRace.DriversChanged += ((DataContext) this.DataContext).OnDriversChanged;
            });
        }
    }
}