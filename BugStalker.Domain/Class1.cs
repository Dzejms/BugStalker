using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugStalker.Domain
{
    public class FakeScreenGrabber : IScreenGrabber
    {
        private readonly List<ScreenShot> screenShots;

        public FakeScreenGrabber()
        {
            screenShots = new List<ScreenShot>();
        }

        public void GrabFullScreen()
        {
            screenShots.Add(new ScreenShot(null));
        }

        public IList<ScreenShot> Screens
        {
            get { return screenShots; }
        }

        public int NumberOfScreens { get { return screenShots.Count; } }
    }

    public interface IScreenGrabber
    {
        void GrabFullScreen();
        IList<ScreenShot> Screens { get; }
        int NumberOfScreens { get; }
    }

    public class BitmapScreenGrabber : IScreenGrabber
    {
        private List<ScreenShot> screenShots = new List<ScreenShot>(); 
        public void GrabFullScreen()
        {
            Bitmap screenShot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(screenShot);
            graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            screenShot.Save(Path.ChangeExtension(Path.GetTempFileName(), "jpg"), ImageFormat.Jpeg);
            screenShot.Save(Path.ChangeExtension(Path.GetTempFileName(), "bmp"), ImageFormat.Bmp);
            screenShot.Save(Path.ChangeExtension(Path.GetTempFileName(), "tif"), ImageFormat.Tiff);
            screenShot.Save(Path.ChangeExtension(Path.GetTempFileName(), "png"), ImageFormat.Png);
        }

        public IList<ScreenShot> Screens { get { return screenShots; } }
        public int NumberOfScreens { get { return screenShots.Count; } }
    }

    public class ScreenShot
    {
        private Bitmap bitmap;
        public ScreenShot(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }
    }
}
