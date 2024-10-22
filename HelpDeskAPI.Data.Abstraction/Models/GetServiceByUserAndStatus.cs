using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class GetServiceByUserAndStatus
    {
        public string? UserId { get; set; }
        public string? Status { get; set; }
    }
}
