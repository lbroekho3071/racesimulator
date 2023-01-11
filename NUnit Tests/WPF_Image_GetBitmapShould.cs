using System.Drawing;
using NUnit.Framework;
using Image = WPF.Image;

namespace NUnit_Tests
{
    [TestFixture]
    public class WPF_Image_GetBitmapShould
    {
        private string _Finish =
            @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\Finish.png";

        private string _StartGrid =
            @"E:\Documenten\School\Windesheim\Jaar2\Semester1\Periode1\C#\RaceSimulator\WPF\resources\Sections\StartGrid.png";
        
        [SetUp]
        public void SetUp()
        {
            Image.GetBitmap(_Finish);
        }

        [Test]
        public void GetBitmap_ExistingBitmap_ReturnBitmap()
        {
            Bitmap bitmap =
                Image.GetBitmap(_Finish);
            
            Assert.IsNotNull(bitmap);
        }

        [Test]
        public void GetBitmap_NonExistingBitmap_ReturnBitmap()
        {
            Bitmap bitmap = Image.GetBitmap(_StartGrid);
            
            Assert.IsNotNull(bitmap);
        }

        [Test]
        public void GetBitmap_NonExistingResource_ReturnNull()
        {
            Bitmap bitmap =
                Image.GetBitmap(@".\doesnt\exist");
            
            Assert.IsNull(bitmap);
        }
    }
}