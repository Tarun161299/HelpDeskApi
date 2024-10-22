using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class GetTicketbyService
    {
        public long? serviceRequestId { get; set; }
        public long? boardId { get; set; }
    }
}
