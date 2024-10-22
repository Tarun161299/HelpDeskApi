using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class UserInfo
    { 
        public string Username { get; set; }
       public string Role { get; set; }

        public string Mode { get; set; }
    }
}
