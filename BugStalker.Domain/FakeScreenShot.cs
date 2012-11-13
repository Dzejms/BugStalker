using System.Drawing;

namespace BugStalker.Domain
{
    public class FakeScreenShot : IScreenShot
    {
        public bool Saved = false;
        public bool Deleted = false;

        public Bitmap GetBitmap()
        {
            throw new System.NotImplementedException();
        }

        public void Save(string filePath)
        {
            Saved = true;
        }

        public void Delete()
        {
            Deleted = true;
        }
    }
}