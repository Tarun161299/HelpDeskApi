using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdAgency
    {
        /// <summary>
        /// required;number
        /// </summary>
        public int AgencyId { get; set; }
        /// <summary>
        /// required;pattern;maxlength;minlength
        /// </summary>
        public string AgencyName { get; set; } = null!;
        /// <summary>
        /// required;pattern;maxlength;minlength
        /// </summary>
        public string? Abbreviation { get; set; }
        /// <summary>
        /// required;pattern;maxlength;minlength
        /// </summary>
        public string? AgencyType { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? StateId { get; set; }
        /// <summary>
        /// required;pattern
        /// </summary>
        public string? ServiceTypeId { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? IsActive { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public int? Priority { get; set; }
    }
}
