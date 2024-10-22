
//-----------------------------------------------------------------------
// <copyright file="MD_PriorityController.cs" company="NIC">
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
    public class MDPriorityController : ControllerBase
    {
        private readonly IMD_PriorityDirector ImD_priorityDirector;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdActionTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdactiontypeDirector">mdactiontypeDirector.</param>
        /// <param name="logger">logger.</param>
        public MDPriorityController(IMD_PriorityDirector _ImD_priorityDirector)
        {
            this.ImD_priorityDirector = _ImD_priorityDirector;
        }

        /// <summary>
        /// Get MdActionType List.
        /// </summary>
        /// <returns>Get All MdActionType List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MD_priority), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MD_priority), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MD_priority>>> GetAsync()
        {
            return await ImD_priorityDirector.GetAllAsync(default).ConfigureAwait(false);
        }
    }


}
