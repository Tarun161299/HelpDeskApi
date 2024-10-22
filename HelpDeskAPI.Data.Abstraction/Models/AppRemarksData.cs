using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class AppRemarksData
    {
        /// <summary>
        /// required;number
        /// </summary>
        public string Module { get; set; } = null!;
        /// <summary>
        /// required;number
        /// </summary>
        public long? ModuleId { get; set; }
    }
}
