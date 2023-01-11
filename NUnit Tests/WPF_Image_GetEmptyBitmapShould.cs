using System.Drawing;
using NUnit.Framework;
using Image = WPF.Image;

namespace NUnit_Tests
{
    [TestFixture]
    public class WPF_Image_GetEmptyBitmapShould
    {
        [Test]
        public void GetEmptyBitmap_DoesNotExistYet_ReturnBitmap()
        {
            Assert.IsFalse(Image.ContainsBitmap("empty"));

            Bitmap empty = Image.GetEmptyBitmap(10, 10);
            
            Assert.IsNotNull(empty);
            Assert.IsTrue(Image.ContainsBitmap("empty"));
        }
    }
}