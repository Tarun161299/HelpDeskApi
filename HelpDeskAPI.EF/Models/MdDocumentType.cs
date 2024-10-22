using System;
using System.Collections.Generic;

namespace HelpDeskAPI.Data.EF.Models
{
    public partial class MdDocumentType
    {
        /// <summary>
        /// reuired
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// required;alphanumeric
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string? Format { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? MinSize { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? MaxSize { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public int? DisplayPriority { get; set; }
        /// <summary>
        /// required;number
        /// </summary>
        public string? DocumentNatureType { get; set; }
        /// <summary>
        /// required;alphabet
        /// </summary>
        public string? DocumentNatureTypeDesc { get; set; }
        /// <summary>
        /// required
        /// </summary>
        public string? IsPasswordProtected { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
