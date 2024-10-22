using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class Esodata
    {
        public long Id { get; set; }
        public string? AccessToken { get; set; }
        public string? ExpiresIn { get; set; }
        public string? RefreshToken { get; set; }
        public string? TokenType { get; set; }
        public string? TokenId { get; set; }
        public string? ClaimData { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
