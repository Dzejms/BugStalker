using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugStalker.Domain
{
    public class ScreenCollector
    {
        private readonly IScreenGrabber _grabber;
        private readonly int _fps;

        public ScreenCollector(IScreenGrabber grabber, int fps = 10)
        {
            _grabber = grabber;
            _fps = fps;
        }

        public int NumberOfFrames
        {
            get { return _grabber.NumberOfScreens; }
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }


    }
}
