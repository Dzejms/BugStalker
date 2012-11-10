using System.Drawing;

namespace BugStalker.Domain
{
    public class ScreenShot
    {
        private Bitmap bitmap;

        public ScreenShot(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public void Save (string filePath)
        {
            //bitmap.Save(filePath, )
        }

        public void Delete()
        {
            bitmap.Dispose();
        }
    }
}