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
    }
}