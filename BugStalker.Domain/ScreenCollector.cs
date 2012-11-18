using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AviFile;

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
                    screens.Dequeue();
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
            Console.WriteLine("Writing avi.  {0} frames to process", screens.Count);
            AviManager aviManager = new AviManager(Path.ChangeExtension(Path.Combine(filePath, Guid.NewGuid().ToString()), "avi"), false);
            IScreenShot screenShot = screens.Dequeue();
            VideoStream aviStream = aviManager.AddVideoStream(true, 10, screenShot.GetBitmap());
            IScreenShot[] screenShots = screens.ToArray();
            for (var i = 1; i < screenShots.Length; i++)
            {
                using (Bitmap frame = screenShots[i].GetBitmap())
                {
                    aviStream.AddFrame(frame);   
                }
                screenShots[i].Delete();
                Console.Write("{0}, ", i);
            }
            aviManager.Close();            
        }


    }
}
