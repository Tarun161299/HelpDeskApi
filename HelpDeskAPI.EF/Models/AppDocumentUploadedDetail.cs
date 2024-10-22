using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class AppDocumentUploadedDetail
    {
        /// <summary>
        /// required;number
        /// </summary>
        public int DocumentId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Activityid { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? CycleId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string DocType { get; set; } = null!;
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? DocId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? DocSubject { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? DocContent { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? ObjectId { get; set; }
        /// <summary>
        /// required;url
        /// </summary>
        public string? ObjectUrl { get; set; }
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string? DocNatureId { get; set; }
        public string? IpAddress { get; set; }
        public DateTime? SubTime { get; set; }
        public string? CreatedBy { get; set; }
    }
}
