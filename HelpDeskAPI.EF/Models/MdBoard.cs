using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdBoard
    {
        public string BoardId { get; set; } = null!;
        public int? AgencyId { get; set; }
        public string? BoardName { get; set; }
        public string? Abbreviation { get; set; }
    }
}
