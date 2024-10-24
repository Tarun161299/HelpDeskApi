//-----------------------------------------------------------------------
// <copyright file="ZmstProjects.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class ZmstProjects
	{
		public int? AgencyId { get; set; }
		public string? ExamCounsid { get; set; }
		public int? AcademicYear { get; set; }
		public int? ServiceType { get; set; }
		public int? Attempt { get; set; }
		public long ProjectId { get; set; }	
		public string? ProjectName { get; set; }
		public string? Description { get; set; }
		////public CUSTOM? RequestLetter { get; set; }
		public string? CreatedDate { get; set; }
		public string? CreatedBy { get; set; }
		public string? ModifiedDate { get; set; }
		public string? ModifiedBy { get; set; }
		public string? IsLive { get; set; }
		public string? PInitiated { get; set; }
	}
}