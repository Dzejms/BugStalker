using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
