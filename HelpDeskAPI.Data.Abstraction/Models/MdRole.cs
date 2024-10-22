//-----------------------------------------------------------------------
// <copyright file="MdRole.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class MdRole
	{
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public string Description { get; set; }
	}
}