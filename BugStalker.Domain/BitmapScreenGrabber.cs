using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BugStalker.Domain
{
    public class BitmapScreenGrabber : IScreenGrabber
    {
        private List<ScreenShot> screenShots = new List<ScreenShot>(); 
        public void GrabFullScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            bmp.Save(Path.ChangeExtension(Path.GetTempFileName(), "png"), ImageFormat.Png);
            ScreenShot screenShot = new ScreenShot(bmp);
            screenShots.Add(screenShot);
        }

        public IList<ScreenShot> Screens { get { return screenShots; } }
        public int NumberOfScreens { get { return screenShots.Count; } }
    }
}