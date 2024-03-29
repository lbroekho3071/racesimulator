﻿using System.ComponentModel;
using System.Dynamic;
using Model.Enums;
using Model.Interfaces;

namespace Model.Classes
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public bool Finished { get; set; } = false;
        public int BrokenCount { get; set; }
        public int Points { get; set; }
        public int Laps { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
    }
}