using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdActionType
    {
        /// <summary>
        /// required;number
        /// </summary>
        public int ActionTypeId { get; set; }
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string? ActionType { get; set; }
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string? IsActive { get; set; }
    }
}
