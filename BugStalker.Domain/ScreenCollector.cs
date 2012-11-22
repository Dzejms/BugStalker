using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using AviFile;
using Timer = System.Timers.Timer;

namespace BugStalker.Domain
{
    public class ScreenCollector
    {
        private readonly int fps;
        private readonly string filePath;
        ConcurrentQueue<IScreenShot> parallelScreens;
        private readonly int maxScreens;

        public ScreenCollector(int fps = 10, int seconds = 300, string filePath = "")
        {
            this.fps = fps;
            parallelScreens = new ConcurrentQueue<IScreenShot>();
            maxScreens = fps * seconds;
            if (String.IsNullOrEmpty(filePath))
                this.filePath = Path.Combine(Path.GetFullPath(Path.GetTempPath()), "BugStalker");
            else
                this.filePath = filePath;
        }

        public int NumberOfFrames
        {
            get { return parallelScreens.Count; }
        }

        public void Start()
        {
            grabScreens();
        }

        private void grabScreens()
        {
                
                Timer screenShotTaker = new Timer(1000 / fps);
                screenShotTaker.Elapsed += (sender, e) => {
                    parallelScreens.Enqueue(ScreenGrabber.GrabFullScreen());
                    if (NumberOfFrames <= maxScreens) return;
                    IScreenShot screenToDestroy;
                    parallelScreens.TryDequeue(out screenToDestroy);
                    screenToDestroy.Delete();
                };
                screenShotTaker.Start();
        }

        public void Stop()
        {
            flush();
        }

        private void flush()
        {
            Console.WriteLine("Writing avi.  {0} frames to process", parallelScreens.Count);
            AviManager aviManager = new AviManager(Path.ChangeExtension(Path.Combine(filePath, Guid.NewGuid().ToString()), "avi"), false);
            IScreenShot screenShot;
            parallelScreens.TryDequeue(out screenShot);
            VideoStream aviStream = aviManager.AddVideoStream(true, fps, screenShot.GetBitmap());
            IScreenShot[] screenShots = parallelScreens.ToArray();
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
