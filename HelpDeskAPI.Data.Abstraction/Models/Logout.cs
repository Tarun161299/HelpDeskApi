//-----------------------------------------------------------------------
// <copyright file="Logout.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public class Logout
    {
        public string client_id { get; set; }

        public string token { get; set; }

        public string user_id { get; set; }
    }
}
