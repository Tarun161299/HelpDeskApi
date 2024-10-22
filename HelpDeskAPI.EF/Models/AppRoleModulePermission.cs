using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class AppRoleModulePermission
    {
        /// <summary>
        /// required;number
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? ModuleId { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? IsActive { get; set; }
    }
}
