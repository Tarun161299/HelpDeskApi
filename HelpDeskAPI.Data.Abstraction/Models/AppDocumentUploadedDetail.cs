//-----------------------------------------------------------------------
// <copyright file="AppDocumentUploadedDetail.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class AppDocumentUploadedDetail
	{
		public int DocumentId { get; set; }
		public string? Activityid { get; set; }
		public long Ticketid { get; set; }
		public string? CycleId { get; set; }
		public string DocType { get; set; } 

        public string? DocId { get; set; }
		public string? DocSubject { get; set; }
		public string? DocContent { get; set; } 

        public string? ObjectId { get; set; }
		public string? ObjectUrl { get; set; }
		public string? DocNatureId { get; set; }
		public string? IpAddress { get; set; }
		public DateTime? SubTime { get; set; }
		public string? CreatedBy { get; set; }

	}
}