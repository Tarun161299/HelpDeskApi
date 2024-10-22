
//-----------------------------------------------------------------------
// <copyright file="MdUserBoardRoleMappingController.cs" company="NIC">
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
    /// MdUserBoardRoleMappingController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdUserBoardRoleMappingController : ControllerBase
    {
        private readonly IMdUserBoardRoleMappingDirector mduserboardrolemappingDirector;
        private readonly ILogger<MdUserBoardRoleMappingController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdUserBoardRoleMappingController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mduserboardrolemappingDirector">mduserboardrolemappingDirector.</param>
        /// <param name="logger">logger.</param>
        public MdUserBoardRoleMappingController(IMdUserBoardRoleMappingDirector mduserboardrolemappingDirector, ILogger<MdUserBoardRoleMappingController> logger)
        {
            this.mduserboardrolemappingDirector = mduserboardrolemappingDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdUserBoardRoleMapping List.
        /// </summary>
        /// <returns>Get All MdUserBoardRoleMapping List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardRoleMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardRoleMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdUserBoardRoleMapping>>> GetAsync()
        {
            return await mduserboardrolemappingDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get MdUserBoardRoleMapping List By Id.
        /// </summary>
        /// <param name="UserId">UserId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardRoleMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardRoleMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<List<AbsModels.MdUserBoardRoleMapping>>> GetAsync(string UserId)
        {
            var response = await mduserboardrolemappingDirector.GetByIdAsync(UserId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get MdUserBoardRoleMapping List By Id.
        /// </summary>
        /// <param name="UserId">UserId.</param>
        /// <returns>Get by id.</returns>
        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardRoleMapping), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdUserBoardRoleMapping), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByBoardId")]
        public async Task<ActionResult<List<AbsModels.MdUserBoardRoleMapping>>> GetBoardAsync(string boardid)
        {
            var response = await mduserboardrolemappingDirector.GetByBoardIdAsync(boardid, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdUserBoardRoleMapping.
        /// </summary>
        /// <param name="mduserboardrolemapping">mduserboardrolemapping.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdUserBoardRoleMapping mduserboardrolemapping)
        {
            if (mduserboardrolemapping == null)
            {
                return BadRequest(mduserboardrolemapping);
            }

            try
            {
                var response = await mduserboardrolemappingDirector.InsertAsync(mduserboardrolemapping, default).ConfigureAwait(false);
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
        /// Update to MdUserBoardRoleMapping.
        /// </summary>
        /// <param name="mduserboardrolemapping">mduserboardrolemapping.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdUserBoardRoleMapping mduserboardrolemapping)
        {
            if (mduserboardrolemapping == null)
            {
                return BadRequest(nameof(mduserboardrolemapping));
            }

            if (mduserboardrolemapping.UserId == "0")
            {
                return BadRequest(nameof(mduserboardrolemapping.UserId));
            }

            try
            {
				var response = await mduserboardrolemappingDirector.UpdateAsync(mduserboardrolemapping, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdUserBoardRoleMapping.
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
            var response = await mduserboardrolemappingDirector.DeleteAsync(UserId, default).ConfigureAwait(false);

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
	