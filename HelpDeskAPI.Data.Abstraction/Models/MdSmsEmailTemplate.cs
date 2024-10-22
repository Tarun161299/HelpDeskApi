//-----------------------------------------------------------------------
// <copyright file="MdSmsEmailTemplate.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class MdSmsEmailTemplate
    {
        public string TemplateId { get; set; } = null!;

        public string? Description { get; set; }

        public string? MessageTypeId { get; set; }

        public string? MessageSubject { get; set; }

        public string? MessageTemplate { get; set; }

        public string? RegisteredTemplateId { get; set; }
    }
}
