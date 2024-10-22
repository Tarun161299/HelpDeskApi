//-----------------------------------------------------------------------
// <copyright file="AppTicketHistory.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class AppTicketHistory
	{
		public long Id { get; set; }
		public long TicketId { get; set; }
		public string? TicketNo { get; set; }
		public long? ServiceRequestId { get; set; }
		public long? BoardId { get; set; }
		public int? RequestCategoryId { get; set; }
		public string? Subject { get; set; }
		public string? Description { get; set; }
		public string? Remarks { get; set; }
		public string? AssignStatus { get; set; }
		public string? TaskStatus { get; set; }
		public string? Priority { get; set; }
		public string? UserId { get; set; }
		public string? AssignTo { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? CreatedBy { get; set; }
		public string? CreatedIp { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string? ModifiedBy { get; set; }
		public string? ModifiedIp { get; set; }
		public DateTime? InsertedOn { get; set; }
	}
}