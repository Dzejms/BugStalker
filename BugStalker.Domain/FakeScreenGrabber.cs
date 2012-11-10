using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugStalker.Domain
{
    public class FakeScreenGrabber : IScreenGrabber
    {
        public ScreenShot GrabFullScreen()
        {
            return new ScreenShot(null, "");
        }
    }
}
