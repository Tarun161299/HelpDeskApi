using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdUserBoardMapping
    {
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public long? BoardId { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? IsActive { get; set; }
    }
}
