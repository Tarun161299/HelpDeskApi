using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class AppRemarks
    {
        /// <summary>
        /// required;number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public string Module { get; set; } = null!;
        /// <summary>
        /// required;number
        /// </summary>
        public long? ModuleId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string Remarks { get; set; } = null!;
        public long? FileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string CreatedIp { get; set; } = null!;
        public string IsActive { get; set; } = null!;
    }
}
