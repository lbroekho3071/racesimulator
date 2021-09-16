using System;
using System.Threading;
using Controller;

namespace Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Visualization.Initialize();
            Visualization.DrawTrack(Data.CurrentRace.Track);
        }
    }
}