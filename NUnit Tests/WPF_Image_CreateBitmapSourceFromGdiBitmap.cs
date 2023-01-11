using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using NUnit.Framework;
using Image = WPF.Image;

namespace NUnit_Tests;

[TestFixture]
public class WPF_Image_CreateBitmapSourceFromGdiBitmap
{
    private readonly string _finish =
        @"C:\Users\luukb\Documents\School\racesimulator\WPF\resources\Sections\Finish.png";
    private readonly string _startGrid = @"C:\Users\luukb\Documents\School\racesimulator\WPF\resources\Sections\StartGrid.png";
    

    [Test]
    public void CreateBitmapSourceFromGdiBitmap_NullBitmap_ThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => Image.CreateBitmapSourceFromGdiBitmap(null));
    }

    [Test]
    public void CreateBitmapSourceFromGdiBitmap_ExistingBitmap_ReturnBitmap()
    {
        Bitmap bitmap = Image.GetBitmap(_finish);
        BitmapSource source = Image.CreateBitmapSourceFromGdiBitmap(bitmap);
        
        Assert.IsNotNull(source);
    }
}