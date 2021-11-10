using System;
using System.Windows;
using System.Windows.Threading;
using Controller;
using Model.Classes;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private RaceWindow _raceWindow;
        // private 
        
        public MainWindow()
        {
            InitializeComponent();
            
            _raceWindow = new RaceWindow();

            Data.Initialize();
            Data.NextRace();
            
            SetEvents();
        }

        private void ShowTrack(Track track)
        {
            TrackImage.Dispatcher.BeginInvoke(
                DispatcherPriority.Render, new Action( () =>
                {
                    TrackImage.Source = null;
                    TrackImage.Source = Visualization.DrawTrack(track);
                }));
        }

        private void SetEvents()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;

            Dispatcher.Invoke(() =>
            {
                Data.CurrentRace.DriversChanged += ((DataContext) DataContext).OnDriversChanged;
            });
        }

        private void ClearEvents()
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceFinished -= OnRaceFinished;
        } 

        private void OnDriversChanged(object sender, DriversChangedEventArgs args)
        {
            ShowTrack(args.Track);
        }
        
        private void OnRaceFinished(object sender, EventArgs e)
        {
            ClearEvents();
            Image.ClearCache();
            
            Data.NextRace();

            if (Data.CurrentRace != null)
            {
                SetEvents();
                ShowTrack(Data.CurrentRace.Track);
            }
        }

        private void MenuItem_Competition_Click(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void MenuItem_Race_Click(object sender, RoutedEventArgs e)
        {
            _raceWindow.Show();
        }

        private void MenuItem_Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}