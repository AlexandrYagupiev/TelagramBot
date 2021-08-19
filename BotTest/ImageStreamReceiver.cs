using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BotTest
{
    public class ImageStreamReceiver
    {
        private readonly string imageStoragePath;

        public ImageStreamReceiver(string imageStoragePath)
        {
            this.imageStoragePath = imageStoragePath;
        }

        public FileStream GetStream(Guid userGuid, int numberInUserFolder, string extension)
        {
         return File.Create($"{imageStoragePath}\\{userGuid}\\{numberInUserFolder}.{extension}");
        }
    }
}
