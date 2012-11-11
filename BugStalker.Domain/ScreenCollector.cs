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
        private readonly ScreenGrabber grabber;
        private readonly int fps;
        private readonly string filePath;
        private readonly Queue<IScreenShot> screens;
        private readonly int maxScreens;
        private CancellationTokenSource cancellationTokenSource;

        public ScreenCollector(ScreenGrabber grabber, int fps = 10, int seconds = 300, string filePath = "")
        {
            this.grabber = grabber;
            this.fps = fps;
            screens = new Queue<IScreenShot>();
            maxScreens = fps * seconds;
            if (String.IsNullOrEmpty(filePath))
                this.filePath = Path.Combine(Path.GetFullPath(Path.GetTempPath()), "BugStalker");
            else
                this.filePath = filePath;
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
                Thread.Sleep(1000 / fps);
                if (NumberOfFrames > maxScreens)
                {
                    IScreenShot screenShot = screens.Dequeue();
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
            flush();
        }

        private void flush()
        {
            foreach (IScreenShot screenShot in screens)
            {
                screenShot.Save(filePath);
            }
        }


    }
}
