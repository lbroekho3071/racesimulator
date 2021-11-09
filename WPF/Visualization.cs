using System.Drawing;
using System.Windows.Media.Imaging;
using Model.Classes;
using Model.Enums;

namespace WPF
{
    public static class Visualization
    {
        public static int Direction = 1;
        public static Point Position = new Point(20, 0);
        
        #region graphics
        private static string _straightHorizontal = "resources\\StraightHorizontal.png";
        private static string _straightVertical = "resources\\StraightVertical.png";

        private static string _cornerNorthWest = "resources\\CornerNW.png";
        private static string _cornerNorthEast = "resources\\CornerNE.png";
        private static string _cornerSouthWest = "resources\\CornerSW.png";
        private static string _cornerSouthEast = "resources\\CornerSE.png";

        private static string _startGrid = "resources\\StartGrid.png";
        
        private static string _finish = "resources\\Finish.png";
        #endregion
        
        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap bitmap = Image.GetEmptyBitmap(64, 64);
            Graphics graphics = Graphics.FromImage(bitmap);

            foreach (Section section in track.Sections)
            {
                SetPosition();
                SetDirection(section);
                
                graphics.DrawImage(GetSectionVisual(section), Position);
            }
            
            return Image.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

        private static Bitmap GetSectionVisual(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.StartGrid:
                    Bitmap start = Image.GetBitmap(_startGrid);
                    
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
                    Bitmap straight = Image.GetBitmap(_startGrid);
                    
                    switch (Direction)
                    {
                        case 0: case 2:
                            return straight;
                        default:
                            straight.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            return straight;
                    }
        
                case SectionTypes.LeftCorner:
                    switch (Direction)
                    {
                        case 0:
                            return Image.GetBitmap(_cornerSouthWest);
                        case 1:
                            return Image.GetBitmap(_cornerSouthEast);
                        case 2:
                            return Image.GetBitmap(_cornerNorthEast);
                        default:
                            return Image.GetBitmap(_cornerNorthWest);
                    }
                
                case SectionTypes.RightCorner:
                    switch (Direction)
                    {
                        case 3:
                            return Image.GetBitmap(_cornerSouthWest);
                        case 2:
                            return Image.GetBitmap(_cornerNorthWest);
                        case 1:
                            return Image.GetBitmap(_cornerNorthEast);
                        default:
                            return Image.GetBitmap(_cornerSouthEast);
                    }
                default:
                    Bitmap finish = Image.GetBitmap(_startGrid);
                    switch (Direction)
                    {
                        case 0: case 2:
                            return finish;
                        default:
                            finish.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            return finish;
                    }
            }
        }
        
        private static void SetPosition()
        {
            switch (Direction)
            {
                case 0:
                    Position.Y--;
                    break;
                case 1:
                    Position.X--;
                    break;
                case 2:
                    Position.Y--;
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