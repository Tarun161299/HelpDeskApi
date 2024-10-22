
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

    /// <summary>
    /// MdActionTypeController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdActionTypeController : ControllerBase
    {
        private readonly IMdActionTypeDirector mdactiontypeDirector;
        private readonly ILogger<MdActionTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdActionTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdactiontypeDirector">mdactiontypeDirector.</param>
        /// <param name="logger">logger.</param>
        public MdActionTypeController(IMdActionTypeDirector mdactiontypeDirector, ILogger<MdActionTypeController> logger)
        {
            this.mdactiontypeDirector = mdactiontypeDirector;
            this.logger = logger;
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
        public async Task<ActionResult<List<AbsModels.MdActionType>>> GetAsync()
        {
            return await mdactiontypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdActionType List By Id.
        /// </summary>
        /// <param name="ActionTypeId">ActionTypeId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdActionType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdActionType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdActionType>> GetAsync(int ActionTypeId)
        {
            var response = await mdactiontypeDirector.GetByIdAsync(ActionTypeId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdActionType.
        /// </summary>
        /// <param name="mdactiontype">mdactiontype.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdActionType mdactiontype)
        {
            if (mdactiontype == null)
            {
                return BadRequest(mdactiontype);
            }

            try
            {
                var response = await mdactiontypeDirector.InsertAsync(mdactiontype, default).ConfigureAwait(false);
                return response > 0 ? Created(string.Empty, response) : Ok(response);
            }
            catch (EntityFoundException entityFoundEx)
            {
                Log.Information(entityFoundEx.Message);
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (System.Exception ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update to MdActionType.
        /// </summary>
        /// <param name="mdactiontype">mdactiontype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdActionType mdactiontype)
        {
            if (mdactiontype == null)
            {
                return BadRequest(nameof(mdactiontype));
            }

            if (mdactiontype.ActionTypeId == 0)
            {
                return BadRequest(nameof(mdactiontype.ActionTypeId));
            }

            try
            {
				var response = await mdactiontypeDirector.UpdateAsync(mdactiontype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdActionType.
        /// </summary>
        /// <param name="ActionTypeId">ActionTypeId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int ActionTypeId)
        {
            string status;
            var response = await mdactiontypeDirector.DeleteAsync(ActionTypeId, default).ConfigureAwait(false);

            if (response > 0)
            {
                status = "\"Delete Successfully\"";
            }
            else
            {
                status = "\"Try Again\"";
            }

            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }
		
	}
	}
	