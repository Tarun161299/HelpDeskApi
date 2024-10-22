using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdModule
    {
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string ModuleId { get; set; } = null!;
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Heading { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? SubHeading { get; set; }
        /// <summary>
        /// required;url
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? Path { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Parent { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? Description { get; set; }
        public string? IsActive { get; set; }
    }
}
