using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BugStalker.Domain;

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

        [Test]
        public void TestScreenShot ()
        {
            // Arrange
            IScreenGrabber grabber = new BitmapScreenGrabber();

            // Act
            grabber.GrabFullScreen();

            // Assert
            Assert.That(grabber.NumberOfScreens == 1);
        }
    }

    
}
