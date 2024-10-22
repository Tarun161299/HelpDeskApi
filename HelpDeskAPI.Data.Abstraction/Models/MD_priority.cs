using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class MD_priority
    {
        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }
        public string? Description { get; set; }
    }
}
