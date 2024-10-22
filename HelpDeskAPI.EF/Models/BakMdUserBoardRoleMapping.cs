using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class BakMdUserBoardRoleMapping
    {
        public string? UserId { get; set; }
        public long? BoardId { get; set; }
        public int? RoleId { get; set; }
        public string? IsActive { get; set; }
    }
}
