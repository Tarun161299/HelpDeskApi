
//-----------------------------------------------------------------------
// <copyright file="MdUserBoardMappingController.cs" company="NIC">
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
    /// MdUserBoardMappingController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdUserBoardMappingController : ControllerBase
    {
        private readonly IMdUserBoardMappingDirector mduserboardmappingDirector;
        private readonly ILogger<MdUserBoardMappingController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdUserBoardMappingController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mduserboardmappingDirector">mduserboardmappingDirector.</param>
        /// <param name="logger">logger.</param>
        public MdUserBoardMappingController(IMdUserBoardMappingDirector mduserboardmappingDirector, ILogger<MdUserBoardMappingController> logger)
        {
            this.mduserboardmappingDirector = mduserboardmappingDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdUserBoardMapping List.
        /// </summary>
        /// <returns>Get All MdUserBoardMapping List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdUserBoardMapping>>> GetAsync()
        {
            return await mduserboardmappingDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get MdUserBoardMapping List By Id.
        /// </summary>
        /// <param name="UserId">UserId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdUserBoardMapping>> GetAsync(string UserId)
        {
            var response = await mduserboardmappingDirector.GetByIdAsync(UserId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdUserBoardMapping.
        /// </summary>
        /// <param name="mduserboardmapping">mduserboardmapping.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdUserBoardMapping mduserboardmapping)
        {
            if (mduserboardmapping == null)
            {
                return BadRequest(mduserboardmapping);
            }

            try
            {
                var response = await mduserboardmappingDirector.InsertAsync(mduserboardmapping, default).ConfigureAwait(false);
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
        /// Update to MdUserBoardMapping.
        /// </summary>
        /// <param name="mduserboardmapping">mduserboardmapping.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdUserBoardMapping mduserboardmapping)
        {
            if (mduserboardmapping == null)
            {
                return BadRequest(nameof(mduserboardmapping));
            }

            if (mduserboardmapping.UserId == "0")
            {
                return BadRequest(nameof(mduserboardmapping.UserId));
            }

            try
            {
				var response = await mduserboardmappingDirector.UpdateAsync(mduserboardmapping, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdUserBoardMapping.
        /// </summary>
        /// <param name="UserId">UserId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string UserId)
        {
            string status;
            var response = await mduserboardmappingDirector.DeleteAsync(UserId, default).ConfigureAwait(false);

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
	