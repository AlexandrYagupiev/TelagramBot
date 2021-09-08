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
            if (!Directory.Exists(imageStoragePath))
                Directory.CreateDirectory(imageStoragePath);
        }

        public string GetPath(Guid userGuid, int numberInUserFolder, int sizeNumber, string extension)
        {
            if (!Directory.Exists($"{imageStoragePath}\\{userGuid}"))
                Directory.CreateDirectory($"{imageStoragePath}\\{userGuid}");
            return ($"{imageStoragePath}\\{userGuid}\\{numberInUserFolder}_{sizeNumber}.{extension}");
        }
    }
}
