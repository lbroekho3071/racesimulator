using Model.Enums;
using Model.Interfaces;

namespace Model.Classes
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }

        public Driver(string name, TeamColors color)
        {
            Name = name;
            TeamColor = color;
        }
    }
}