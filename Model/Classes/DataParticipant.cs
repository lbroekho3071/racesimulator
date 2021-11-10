using System.Collections.Generic;
using Model.Interfaces;

namespace Model.Classes
{
    public class DataParticipant
    {
        public string Name { get; set; }
        public bool Finished { get; set; }

        public DataParticipant(string name, bool finished)
        {
            Name = name;
            Finished = finished;
        }
    }
}