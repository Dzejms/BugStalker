using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            ScreenCollector collector = new ScreenCollector(grabber, 1, 1);

            // Act
            collector.Start();
            Thread.Sleep(1000);
            collector.Stop();

            // Assert
            Assert.AreEqual(1, collector.NumberOfFrames);
        }

        [Test]
        public void TestScreenShot()
        {
            // Arrange
            IScreenGrabber grabber = new PngScreenGrabber();
            ScreenCollector collector = new ScreenCollector(grabber, 1, 1);

            // Act
            collector.Start();
            Thread.Sleep(1000);
            collector.Stop();

            // Assert
            Assert.AreEqual(1, collector.NumberOfFrames);
        }

        [Test]
        public void CanGrabScreensAtSpecifiedFrameRate()
        {
            // Arrange
            IScreenGrabber grabber = new PngScreenGrabber();
            ScreenCollector collector = new ScreenCollector(grabber, 10, 1);

            // Act
            collector.Start();
            Thread.Sleep(1000);
            collector.Stop();

            // Assert
            Assert.AreEqual(10, collector.NumberOfFrames);

        }
    }

    
}
