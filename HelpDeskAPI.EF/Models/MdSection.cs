using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdSection
    {
        /// <summary>
        /// required;number
        /// </summary>
        public int SectionId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Section { get; set; }
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string? IsActive { get; set; }
    }
}
