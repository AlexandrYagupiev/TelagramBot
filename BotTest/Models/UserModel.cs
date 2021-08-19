using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BotTest
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }

        public List<PhotoPathModel> PhotoPathes { get; set; }

        public PhotoPathModel LastPhoto { get; set; }
        [Key]
        public Guid Guid { get; set; }
    }
}
