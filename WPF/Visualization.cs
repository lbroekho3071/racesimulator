using System.Drawing;
using System.Net.Mime;
using System.Windows.Media.Imaging;
using Controller;
using Model.Classes;
using Model.Enums;

namespace WPF
{
    public static class Visualization
    {
        public static int Direction = 1;
        public static int SectionSize = 75;
        public static Point Position = new Point(3, 0);
        public static Point MaxSize = new Point(3, 0);
        
        #region graphics
        private const string StraightHorizontal = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\StraightHorizontal.png";
        private const string StraightVertical = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\StraightVertical.png";

        private const string CornerNorthWest = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\CornerNW.png";
        private const string CornerNorthEast = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\CornerNE.png";
        private const string CornerSouthWest = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\CornerSW.png";
        private const string CornerSouthEast = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\CornerSE.png";

        private const string StartGrid = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\StartGrid.png";
        
        private const string Finish = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\Finish.png";

        private const string Player = @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Cars";
        #endregion

        public static BitmapSource DrawTrack(Track track)
        {
            MaxSize = GetMaxSize(track);
            
            Bitmap bitmap = Image.GetEmptyBitmap(MaxSize.X * SectionSize, MaxSize.Y * SectionSize);
            Graphics graphics = Graphics.FromImage(bitmap);

            foreach (Section section in track.Sections)
            {
                SetPosition();
                SetDirection(section);

                SectionData sectionData = Data.CurrentRace.GetSectionData(section);

                graphics.DrawImage(GetSectionVisual(section), new Point(Position.X * SectionSize, Position.Y * SectionSize));
                
                if (sectionData.Left != null)
                    graphics.DrawImage(
                        Image.GetBitmap($"{Player}\\{(sectionData.Left.Equipment.IsBroken ? "X" : $"Team{sectionData.Left.TeamColor}")}.png"),
                        new Point(Position.X * SectionSize + sectionData.DistanceLeft * SectionSize / 100,
                            Position.Y * SectionSize + 19));

                if (sectionData.Right != null)
                    graphics.DrawImage(
                        Image.GetBitmap($"{Player}\\{(sectionData.Right.Equipment.IsBroken ? "X" : $"Team{sectionData.Right.TeamColor}")}.png"),
                        new Point(Position.X * SectionSize + sectionData.DistanceRight * SectionSize / 100,
                            Position.Y * SectionSize + 40));
            }
            
            return Image.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

        private static Bitmap GetSectionVisual(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.StartGrid:
                    Bitmap start = Image.GetBitmap(StartGrid);
                    
                    switch (Direction)
                    {
                        case 0: 
                            start.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            return start;
                        case 1:
                            return start;
                        case 2:
                            start.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            return start;
                        default:
                            start.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            return start;
                    }
        
                case SectionTypes.Straight:
                   
                    switch (Direction)
                    {
                        case 0: case 2:
                            return Image.GetBitmap(StraightVertical);
                        default:
                            return Image.GetBitmap(StraightHorizontal);
                    }
        
                case SectionTypes.LeftCorner:
                    switch (Direction)
                    {
                        case 0:
                            return Image.GetBitmap(CornerSouthWest);
                        case 1:
                            return Image.GetBitmap(CornerSouthEast);
                        case 2:
                            return Image.GetBitmap(CornerNorthEast);
                        default:
                            return Image.GetBitmap(CornerNorthWest);
                    }
                
                case SectionTypes.RightCorner:
                    switch (Direction)
                    {
                        case 3:
                            return Image.GetBitmap(CornerSouthWest);
                        case 2:
                            return Image.GetBitmap(CornerNorthWest);
                        case 1:
                            return Image.GetBitmap(CornerNorthEast);
                        default:
                            return Image.GetBitmap(CornerSouthEast);
                    }
                default:
                    Bitmap finish = Image.GetBitmap(Finish);
                    switch (Direction)
                    {
                        case 0: case 2:
                            finish.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            return finish;
                        default:
                            return finish;
                    }
            }
        }

        private static Point GetMaxSize(Track track)
        {
            Point point = new Point(20, 0);

            foreach (Section section in track.Sections)
            {
                SetPosition();
                SetDirection(section);

                if (Position.X >= point.X)
                    point.X = Position.X + 1;

                if (Position.Y >= point.Y)
                    point.Y = Position.Y + 1;
            }

            return point;
        }
        
        private static void SetPosition()
        {
            switch (Direction)
            {
                case 0:
                    Position.Y--;
                    break;
                case 1:
                    Position.X++;
                    break;
                case 2:
                    Position.Y++;
                    break;
                case 3:
                    Position.X--;
                    break;
            }
        }
        
        private static void SetDirection(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.LeftCorner:
                    Direction = Clamp(Direction - 1, 0, 3);
                    break;
                case SectionTypes.RightCorner:
                    Direction = Clamp(Direction + 1, 0, 3);
                    break;
            }
        }

        private static int Clamp( int value, int min, int max )
        {
            return (value < min) ? max : (value > max) ? min : value;
        }
    }
}