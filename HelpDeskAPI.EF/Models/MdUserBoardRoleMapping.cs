using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdUserBoardRoleMapping
    {
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public long? BoardId { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? IsActive { get; set; }
    }
}
