using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BotTest
{
    public class PhotoPathModel
    {
        [Key]
        public Guid Guid { get; set; }

        public string PhotoPath { get; set; }

        public int NumberInUserFolder { get; set; }

        public int SizeNumber { get; set; }
    }
}
