using System.Collections.Generic;

namespace BugStalker.Domain
{
    public interface IScreenGrabber
    {
        void GrabFullScreen();
        IList<ScreenShot> Screens { get; }
        int NumberOfScreens { get; }
    }
}