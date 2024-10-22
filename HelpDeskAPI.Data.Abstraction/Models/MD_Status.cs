using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class MD_Status
    {
        public string Id { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? EntityType { get; set; }
		public int? Priority { get; set; }
	}
}
