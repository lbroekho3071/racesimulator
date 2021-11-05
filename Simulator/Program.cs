using System;
using System.Collections.Generic;
using System.Threading;
using Controller;
using Model.Classes;
using Model.Enums;

namespace Simulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Visualization.Initialize();
            Visualization.DrawTrack(Data.CurrentRace.Track);
            
            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
        
        
        
    }
}