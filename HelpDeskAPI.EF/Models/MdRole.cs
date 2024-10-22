using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdRole
    {
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
