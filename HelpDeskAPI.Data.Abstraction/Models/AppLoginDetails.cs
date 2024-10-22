//-----------------------------------------------------------------------
// <copyright file="AppLoginDetails.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class AppLoginDetails
	{
		public string UserId { get; set; }
		public string? UserName { get; set; }
		public string? IsActive { get; set; }
		public DateTime? LastLoginTime { get; set; }
		public string? LastLoginIP { get; set; }
		public string? Mobile { get; set; }
		public string? Email { get; set; }
	}
}