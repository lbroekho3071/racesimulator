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
            Visualization.Initialize(Data.CurrentRace.Track);
            Visualization.DrawTrack();
            
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}