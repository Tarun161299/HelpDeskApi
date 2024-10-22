//-----------------------------------------------------------------------
// <copyright file="EmailController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Service.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using HelpDeskAPI.Data.Abstractions.Behaviors;
    using HelpDeskAPI.Data.Abstractions.Models;
    using HelpDeskAPI.Data.EF.Models;

    /// <summary>
    /// EmailController .
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AAAController : Controller
    {
        public AAAController()
        {
            // Changes
        }

        [HttpPost("V05")]
        public async Task<IActionResult> Send()
        {
            try
            {
                return Ok("Version V05");
                // hg
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
