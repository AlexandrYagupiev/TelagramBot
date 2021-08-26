using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using BotTest;
using System.Linq;

namespace BotTests
{
    public class ImageStreamReceiverTest
    {
        [SetUp]
        public void Setup()
        {
            imageStreamReceiver = new ImagePathFormatter("\\test");
        }

        private ImagePathFormatter imageStreamReceiver;

        [Test]
        public void ImageSaves()
        {
           //var stream = imageStreamReceiver.GetStream(Guid.NewGuid(), 0, "jpg");
        }
    }
}
