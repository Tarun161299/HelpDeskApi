using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class ZmstProjects
    {
        /// <summary>
        /// required
        /// </summary>
        public int? AgencyId { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? ExamCounsid { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public int? AcademicYear { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public int? ServiceType { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public int? Attempt { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public long ProjectId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? ProjectName { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Description { get; set; }
        public byte[]? RequestLetter { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? IsLive { get; set; }
        public string? Pinitiated { get; set; }
    }
}
