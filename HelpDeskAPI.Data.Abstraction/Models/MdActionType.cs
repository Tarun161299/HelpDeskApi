//-----------------------------------------------------------------------
// <copyright file="MdActionType.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class MdActionType
	{
		public int ActionTypeId { get; set; }
		public string? ActionType { get; set; }
		public string? IsActive { get; set; }
	}
}