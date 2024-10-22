//-----------------------------------------------------------------------
// <copyright file="MdUserBoardRoleMapping.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class MdUserBoardRoleMapping
	{
		public string? UserId { get; set; }
		public long? BoardId { get; set; }
		public int? RoleId { get; set; }
		public string? IsActive { get; set; }
	}
}