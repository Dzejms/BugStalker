namespace BugStalker.Domain
{
    public class FakeScreenShot : IScreenShot
    {
        public bool Saved = false;
        public bool Deleted = false;

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