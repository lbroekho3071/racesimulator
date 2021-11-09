using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Model.Classes;
using DispatcherPriority = System.Windows.Threading.DispatcherPriority;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CompetitionStatistics _competitionStatistics;
        private RaceStatistics _raceStatistics;
        
        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();

            SetEvents();
        }

        public void SetEvents()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;
        }

        public void ClearEvents()
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceFinished -= OnRaceFinished;
        }
        
        public void OnDriversChanged(object obj, DriversChangedEventArgs args)
        {
            DisplayTrack(args.Track);
        }

        public void OnRaceFinished(object obj, EventArgs args)
        {
            ClearEvents();
            WPF.Image.ClearCache();
            
            if (Data.CurrentRace != null)
            {
                if (Data.Competition.Tracks.Count > 0)
                {
                    Data.NextRace();
                    SetEvents();
                    
                    DisplayTrack(Data.CurrentRace.Track);
                }
            }
        }

        public void DisplayTrack(Track track)
        {
            this.Image.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() =>
            {
                this.Image.Source = null;
                this.Image.Source = Visualization.DrawTrack(track);
            }));
        }

        private void MenuItem_Competition_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_Race_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItem_Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}