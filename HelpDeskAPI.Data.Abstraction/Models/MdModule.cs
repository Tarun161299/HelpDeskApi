//-----------------------------------------------------------------------
// <copyright file="MdModule.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class MdModule
	{
		public string ModuleId { get; set; }
		public string? Heading { get; set; }
		public string? SubHeading { get; set; }
		public string? Url { get; set; }
		public string? Path { get; set; }
		public string? Parent { get; set; }
		public string? Description { get; set; }
		public string? IsActive { get; set; }
	}
}