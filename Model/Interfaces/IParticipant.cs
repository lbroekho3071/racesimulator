using System.ComponentModel;
using Model.Enums;

namespace Model.Interfaces
{
    public interface IParticipant
    {
        public string Name { get; set; }
        public bool Finished { get; set; }
        public int BrokenCount { get; set; }
        public int Points { get; set; }
        public int Laps { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
    }
}