//-----------------------------------------------------------------------
// <copyright file="UtilityService.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Business.Services
{
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using Microsoft.Extensions.Options;
    using Microsoft.AspNetCore.Http;
    using System;
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <inheritdoc />
    public class UtilityService
    {
        private readonly Random _random = new Random();
        private readonly MailService _mailSettings;
        //private readonly Random _random = new Random();
        // public Session Session { get;  set; }
        // public string salt { get;set; }

        public UtilityService(IOptions<MailService> options, IHttpContextAccessor httpContextAccessor)
        {
            _mailSettings = options.Value;
            //this._httpContextAccessor = httpContextAccessor;

        }

        /// <inheritdoc />
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            byte[] file;
            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Host = _mailSettings.Host;
            MailMessage MailMsg = new MailMessage(new MailAddress(_mailSettings.Username), new MailAddress(mailRequest.ToEmail));
        
            MailMsg.BodyEncoding = Encoding.Default;
            MailMsg.Subject = mailRequest.Subject.Trim();
            MailMsg.Body = mailRequest.Body.Trim();

            MailMsg.Priority = MailPriority.High;
            MailMsg.IsBodyHtml = true;
            if (mailRequest.Attachment != null)
            {
               file= Convert.FromBase64String(mailRequest.Attachment);
               Attachment mailAttachment = new Attachment(new MemoryStream(file),"HelpDesk.pdf");
               MailMsg.Attachments.Add(mailAttachment);
            }
            if (!string.IsNullOrEmpty(mailRequest.CCMail))
                MailMsg.CC.Add(mailRequest.CCMail);

            // New Code Start
            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.EnableSsl = true;
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpMail.Send(MailMsg);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.

            // char is a single Unicode character
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }

}
