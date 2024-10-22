//-----------------------------------------------------------------------
// <copyright file="MdSection.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
	public class MdSection 
	{
		public int SectionId { get; set; }
		public string? Section { get; set; }
		public string? IsActive { get; set; }
	}
}