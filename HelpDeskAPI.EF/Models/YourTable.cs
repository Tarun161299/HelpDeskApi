using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class YourTable
    {
        public int Id { get; set; }
        public string? AlphanumericColumn { get; set; }
        public string? Name { get; set; }
    }
}
