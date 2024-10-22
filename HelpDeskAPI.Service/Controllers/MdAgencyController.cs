
//-----------------------------------------------------------------------
// <copyright file="MdAgencyController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using HelpDeskAPI.Data.Abstractions.Behaviors;
    using HelpDeskAPI.Data.Abstractions.Exceptions;
    using Serilog;
    using AbsModels = HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// MdAgencyController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdAgencyController : ControllerBase
    {
        private readonly IMdAgencyDirector mdagencyDirector;
        private readonly ILogger<MdAgencyController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdAgencyController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdagencyDirector">mdagencyDirector.</param>
        /// <param name="logger">logger.</param>
        public MdAgencyController(IMdAgencyDirector mdagencyDirector, ILogger<MdAgencyController> logger)
        {
            this.mdagencyDirector = mdagencyDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdAgency List.
        /// </summary>
        /// <returns>Get All MdAgency List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdAgency>>> GetAsync()
        {
            return await mdagencyDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdAgency List By Id.
        /// </summary>
        /// <param name="AgencyId">AgencyId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdAgency>> GetAsync(int AgencyId)
        {
            var response = await mdagencyDirector.GetByIdAsync(AgencyId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
    }
}
	