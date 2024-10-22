using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class AppLoginDetail
    {
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string UserId { get; set; } = null!;
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string? IsActive { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? LastLoginIp { get; set; }
        /// <summary>
        /// required;number;maxlength;minlength
        /// </summary>
        public string? Mobile { get; set; }
        /// <summary>
        /// required;email
        /// </summary>
        public string? Email { get; set; }
    }
}
