
//-----------------------------------------------------------------------
// <copyright file="MdActionTypeController.cs" company="NIC">
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
    using HelpDeskAPI.Data.Business.Behaviors;

    [Route("api/[controller]")]
    [ApiController]
    public class MD_StatusController : ControllerBase
    {
        private readonly IMD_StatusDirector ImD_StatusDirector;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdActionTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdactiontypeDirector">mdactiontypeDirector.</param>
        /// <param name="logger">logger.</param>
        public MD_StatusController(IMD_StatusDirector _ImdStatusDirector)
        {
            this.ImD_StatusDirector = _ImdStatusDirector;
        }

        /// <summary>
        /// Get MdActionType List.
        /// </summary>
        /// <returns>Get All MdActionType List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdActionType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdActionType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MD_Status>>> GetAsync()
        {
            return await ImD_StatusDirector.GetAllAsync(default).ConfigureAwait(false);
        }
    }
}
