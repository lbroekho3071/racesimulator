using Model.Classes;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }

        static Data()
        {
            Competition = new Competition();
        }
    }
}