using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BugStalker.Domain
{
    public class ScreenCollector
    {
        private readonly IScreenGrabber grabber;
        private readonly int fps;
        private readonly int seconds;
        private readonly string filePath;
        private readonly Queue<ScreenShot> screens;
        private int maxScreens;
        private CancellationTokenSource cancellationTokenSource;

        public ScreenCollector(IScreenGrabber grabber, int fps = 10, int seconds = 300, string filePath = "")
        {
            this.grabber = grabber;
            this.fps = fps;
            this.seconds = seconds;
            this.filePath = filePath;
            screens = new Queue<ScreenShot>();
            maxScreens = fps * seconds;
            if (String.IsNullOrEmpty(filePath))
                this.filePath = Path.Combine(Path.GetFullPath(Path.GetTempPath()), "BugStalker");
        }

        public int NumberOfFrames
        {
            get { return screens.Count; }
        }

        public void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task task = new Task(() => grabScreens(cancellationTokenSource.Token));
            task.Start();
        }

        private void grabScreens(CancellationToken token)
        {
            while (true)
            {
                screens.Enqueue(grabber.GrabFullScreen());
                Thread.Sleep( 1000 / fps);
                if (NumberOfFrames > maxScreens)
                {
                    ScreenShot screenShot = screens.Dequeue();
                    screenShot.Delete();
                }
                if (token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        public void Stop()
        {
            cancellationTokenSource.Cancel();    
        }

        public void Flush()
        {
            foreach (ScreenShot screenShot in screens)
            {
                
            }
        }


    }
}
