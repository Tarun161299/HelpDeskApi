
//-----------------------------------------------------------------------
// <copyright file="MdRoleController.cs" company="NIC">
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
    /// MdRoleController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdRoleController : ControllerBase
    {
        private readonly IMdRoleDirector mdroleDirector;
        private readonly ILogger<MdRoleController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdRoleController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdroleDirector">mdroleDirector.</param>
        /// <param name="logger">logger.</param>
        public MdRoleController(IMdRoleDirector mdroleDirector, ILogger<MdRoleController> logger)
        {
            this.mdroleDirector = mdroleDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdRole List.
        /// </summary>
        /// <returns>Get All MdRole List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdRole), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdRole), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdRole>>> GetAsync()
        {
            return await mdroleDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdRole List By Id.
        /// </summary>
        /// <param name="RoleId">RoleId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdRole), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdRole), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdRole>> GetAsync(int RoleId)
        {
            var response = await mdroleDirector.GetByIdAsync(RoleId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdRole.
        /// </summary>
        /// <param name="mdrole">mdrole.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdRole mdrole)
        {
            if (mdrole == null)
            {
                return BadRequest(mdrole);
            }

            try
            {
                var response = await mdroleDirector.InsertAsync(mdrole, default).ConfigureAwait(false);
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
        /// Update to MdRole.
        /// </summary>
        /// <param name="mdrole">mdrole.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdRole mdrole)
        {
            if (mdrole == null)
            {
                return BadRequest(nameof(mdrole));
            }

            if (mdrole.RoleId == 0)
            {
                return BadRequest(nameof(mdrole.RoleId));
            }

            try
            {
				var response = await mdroleDirector.UpdateAsync(mdrole, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdRole.
        /// </summary>
        /// <param name="RoleId">RoleId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int RoleId)
        {
            string status;
            var response = await mdroleDirector.DeleteAsync(RoleId, default).ConfigureAwait(false);

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
	