//-----------------------------------------------------------------------
// <copyright file="MdUserBoardMapping.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class MdUserBoardMapping
	{
		public string? UserId { get; set; }
		public long? BoardId { get; set; }
		public string? IsActive { get; set; }
	}
}