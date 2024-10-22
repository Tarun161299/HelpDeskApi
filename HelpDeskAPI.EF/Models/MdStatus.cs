using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdStatus
    {
        public string Id { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? EntityType { get; set; }
        public int? Priority { get; set; }
    }
}
