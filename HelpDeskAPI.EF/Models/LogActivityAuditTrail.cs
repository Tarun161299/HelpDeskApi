using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class LogActivityAuditTrail
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Activity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? IpAddress { get; set; }
        public string? Remarks { get; set; }
    }
}
