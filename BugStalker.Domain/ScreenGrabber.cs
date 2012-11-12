using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BugStalker.Domain
{
    public enum ImageFormat
    {
        Png = 0,
        Test = 1
    }

    public class ScreenGrabber
    {
        private ImageFormat imageFormat;

        public ScreenGrabber(ImageFormat imageFormat)
        {
            this.imageFormat = imageFormat;
        }

        public IScreenShot GrabFullScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            if (imageFormat == ImageFormat.Png) 
                return new PngScreenShot(bmp);
            else 
                return new FakeScreenShot();
        }

    }
}