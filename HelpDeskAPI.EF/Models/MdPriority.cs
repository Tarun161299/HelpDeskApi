using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdPriority
    {
        public int PriorityId { get; set; }
        public string? PriorityName { get; set; }
        public string? Description { get; set; }
    }
}
