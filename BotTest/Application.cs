using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public string PhotoPath { get; set; }

        [Key]
        public Guid Guid { get; set; }
    }
}
