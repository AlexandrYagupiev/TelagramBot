using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;

namespace BotTest
{
    public class ApplicationModel
    {
        public string ShortDescription { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public UserModel User { get; set; }

        public ProductCategoryModel ProductCategory { get; set; }

        public List<PhotoPathModel> PhotoPathes  { get; set; }

        [Key]
        public Guid Guid { get; set; }
    }
}
