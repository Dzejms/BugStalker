using System.Drawing;
using System.IO;

namespace BugStalker.Domain
{
    public class ScreenShot
    {
        private Bitmap bitmap;
        private readonly string filePath;

        public ScreenShot(Bitmap bitmap, string filePath)
        {
            this.bitmap = bitmap;
            this.filePath = filePath;
        }

        public void Delete()
        {
            File.Delete(filePath);
            bitmap.Dispose();
        }
    }
}