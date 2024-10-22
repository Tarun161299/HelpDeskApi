using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class UserAuthorization
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public string? Token { get; set; }
        public string? Mode { get; set; }
    }
}
