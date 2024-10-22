using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class AppServiceRequestsList: MdSection
    {
        public long ServiceRequestId { get; set; }

        public string? ServiceRequestNo { get; set; }
        public string? BoardId { get; set; }

        public string? BoardName { get;set; }
        public string? RequestCategoryIds { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public DateTime? ResolutionDate { get; set; }
        public string? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedIp { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedIp { get; set; }

        public long? FileId { get; set; }

        public string? StatusId { get; set; }

        public string? DocContent { get; set; }

    }
}
