using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using Image = WPF.Image;

namespace NUnit_Tests
{
    [TestFixture]
    public class WPF_Image_GetBitmapShould
    {
        private readonly string _finish =
            @"C:\Users\luukb\Documents\School\racesimulator\WPF\resources\Sections\Finish.png";
        private readonly string _startGrid = @"C:\Users\luukb\Documents\School\racesimulator\WPF\resources\Sections\StartGrid.png";
        
        [SetUp]
        public void SetUp()
        {
            Image.GetBitmap(_finish);
        }

        [Test]
        public void GetBitmap_ExistingBitmap_ReturnBitmap()
        {
            Bitmap bitmap =
                Image.GetBitmap(_finish);
            
            Assert.IsNotNull(bitmap);
        }

        [Test]
        public void GetBitmap_NonExistingBitmap_ReturnBitmap()
        {
            Bitmap bitmap = Image.GetBitmap(_startGrid);
            
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