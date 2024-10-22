using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class AppServiceRequestHistory
    {
        /// <summary>
        /// required;number
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public long ServiceRequestId { get; set; }
        public string? ServiceRequestNo { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public long? BoardId { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public int? RequestCategoryids { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Subject { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? Priority { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public DateTime ResolutionDate { get; set; }
        /// <summary>
        /// required,alphanumeric
        /// </summary>
        public string? UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedIp { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedIp { get; set; }
        public DateTime? InsertedOn { get; set; }
    }
}
