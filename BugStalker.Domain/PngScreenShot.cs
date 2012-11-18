using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BugStalker.Domain
{
    public interface IScreenShot
    {
        Bitmap GetBitmap();
        void Delete();
    }

    public class PngScreenShot : IScreenShot
    {
        private MemoryStream compressedBitmap;

        public PngScreenShot(Bitmap bitmap)
        {
            compressedBitmap = new MemoryStream();
            bitmap.Save(compressedBitmap, System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();
        }

        public Bitmap GetBitmap()
        {
            return Image.FromStream(compressedBitmap) as Bitmap;
        }

        public void Delete()
        {
            this.compressedBitmap.Dispose();
            this.compressedBitmap = null;
        }
    }
}