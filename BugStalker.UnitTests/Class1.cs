using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BugStalker.UnitTests
{
    [TestFixture]
    internal class ScreenCaptureTests
    {
        [Test]
        public void CanCaptureCurrentFullScreenToMemory()
        {
            // Arrange
            IScreenGrabber grabber = new FakeScreenGrabber();

            // Act
            grabber.GrabFullScreen();

            // Assert
            Assert.That(grabber.NumberOfScreens == 1);

        }
    }

    internal class FakeScreenGrabber : IScreenGrabber
    {
        private readonly List<ScreenShot> screenShots;

        public FakeScreenGrabber()
        {
            screenShots = new List<ScreenShot>();
        }

        public void GrabFullScreen()
        {
            screenShots.Add(new ScreenShot());
        }

        public IList<ScreenShot> Screens { 
            get { return screenShots; }
        }

        public int NumberOfScreens { get { return screenShots.Count; } }
    }

    internal interface IScreenGrabber
    {
        void GrabFullScreen();
        IList<ScreenShot> Screens { get; }
        int NumberOfScreens { get; }
    }

    internal class ScreenShot
    {

    }
}
