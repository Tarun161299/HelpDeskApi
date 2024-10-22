using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class UserAuthorization
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public string? Token { get; set; }
        public string? Mode { get; set; }
    }
}
