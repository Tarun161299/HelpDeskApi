using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class getserviceRequest
    {
        public long ServiceRequestId { get; set; }
        public long? BoardId { get; set; }
    }
}
