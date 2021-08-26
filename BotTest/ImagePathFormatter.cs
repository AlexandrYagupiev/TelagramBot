using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BotTest
{
    public class ImagePathFormatter
    {
        private readonly string imageStoragePath;

        public ImagePathFormatter(string imageStoragePath)
        {
            this.imageStoragePath = imageStoragePath;
        }

        public string GetPath(Guid userGuid, int numberInUserFolder,int sizeNumber, string extension)
        {
         return ($"{imageStoragePath}\\{userGuid}\\{numberInUserFolder}_{sizeNumber}.{extension}");
        }
    }
}
