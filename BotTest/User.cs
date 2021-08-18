using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BotTest
{
    public class User
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }

        [Key]
        public Guid Guid { get; set; }
    }
}
