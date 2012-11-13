using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BugStalker.Domain
{
    public interface IScreenShot
    {
        Bitmap GetBitmap();
        void Save(string filePath);
        void Delete();
    }

    public class PngScreenShot : IScreenShot
    {
        private readonly Bitmap bitmap;

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public PngScreenShot(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public void Save(string filePath)
        {
            bitmap.Save(Path.ChangeExtension(filePath, ".png"), System.Drawing.Imaging.ImageFormat.Png);
        }

        public void Delete()
        {
            bitmap.Dispose();
        }
    }
}