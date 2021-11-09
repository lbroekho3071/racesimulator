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
        
        
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Data.Initialize();
                Data.NextRace();

                Data.CurrentRace.DriversChanged += OnDriversChanged;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void OnDriversChanged(object obj, DriversChangedEventArgs args)
        {
            this.Image.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() =>
            {
                this.Image.Source = null;
                this.Image.Source = Visualization.DrawTrack(args.Track);
            }));
        }
    }
}