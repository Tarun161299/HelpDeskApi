//-----------------------------------------------------------------------
// <copyright file="AppServiceRequestHistory.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class AppServiceRequestHistory
	{
		public long Id { get; set; }
		public long ServiceRequestId { get; set; }
		public string? ServiceRequestNo { get; set; }
		public long? BoardId { get; set; }
		public int? RequestCategoryids { get; set; }
		public string? Subject { get; set; }
		public string? Description { get; set; }
		public string? Status { get; set; }
		public string? Priority { get; set; }
		public DateTime ResolutionDate { get; set; }
		public string? UserId { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? CreatedBy { get; set; }
		public string? CreatedIp { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string? ModifiedBy { get; set; }
		public string? ModifiedIp { get; set; }
		public DateTime? InsertedOn { get; set; }
	}
}