using System.Collections.Generic;

namespace BugStalker.Domain
{
    public interface IScreenGrabber
    {
        ScreenShot GrabFullScreen();
    }
}