//-----------------------------------------------------------------------
// <copyright file="SignUp.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Models
{
    public  class SignUp
    {
        //[Required(ErrorMessage = "UserID is required.")]
        public string UserID { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        //[DataType(DataType.Password)]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "RequestNo is required.")]
        public string Mobile { get; set; }

        //[Required(ErrorMessage = "UserName is required.")]
        //[RegularExpression("^[A-Za-z. ]+$", ErrorMessage = "Invalid UserName")]
        public string Email { get; set; }
    }
}
