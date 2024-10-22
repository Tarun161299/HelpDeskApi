//-----------------------------------------------------------------------
// <copyright file="AppRoleModulePermission.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class AppRoleModulePermission
	{
		public int? RoleId { get; set; }
		public string? ModuleId { get; set; }
		public string? IsActive { get; set; }
	}
}