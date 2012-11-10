using System.Drawing;
using System.Drawing.Imaging;

namespace BugStalker.Domain
{
    public interface IScreenShot
    {
        void Save(string filePath);
        void Delete();
    }

    public class PngScreenShot : IScreenShot
    {
        private readonly Bitmap bitmap;

        public PngScreenShot(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public void Save(string filePath)
        {
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        public void Delete()
        {
            bitmap.Dispose();
        }
    }
}