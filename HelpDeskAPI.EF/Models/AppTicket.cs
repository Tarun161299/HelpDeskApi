using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class AppTicket
    {
        /// <summary>
        /// required;number
        /// </summary>
        public long TicketId { get; set; }
        public string? TicketNo { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public long? ServiceRequestId { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public long? BoardId { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public int? SectionId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Subject { get; set; }
        public long? FileId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string? AssignStatus { get; set; }
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string? TaskStatus { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? Priority { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? AssignTo { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedIp { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedIp { get; set; }
    }
}
