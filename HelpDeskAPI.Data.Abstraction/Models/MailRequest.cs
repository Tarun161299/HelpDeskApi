//-----------------------------------------------------------------------
// <copyright file="MailRequest.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class MailRequest
    {
        public string? ToEmail { get; set; }

        public string? CCMail { get; set; }

        public string? Subject { get; set; }

        public string? Body { get; set; }

        public string? Attachment { get; set; }
    }
}
