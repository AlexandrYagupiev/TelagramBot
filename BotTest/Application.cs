using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BotTest
{
    public class Application
    {
        public string ShortDescription { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Heading { get; set; }

        public User User { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public Bitmap Bitmap { get; set; }

        public Guid Guid { get; set; }
    }
}
