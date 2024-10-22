using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class GenerateTicket
    {
        public long TicketId { get; set; }
        public string? TicketNo { get; set; }
        public long? ServiceRequestId { get; set; }
        public long? BoardId { get; set; }
        public int? SectionId { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? AssignStatus { get; set; }
        public string? TaskStatus { get; set; }
        public string? Priority { get; set; }
        public string? AssignTo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedIp { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedIp { get; set; }

        public string? docContent { get; set; }
        public string? docType { get; set; }
    }
}
