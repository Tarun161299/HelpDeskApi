
//-----------------------------------------------------------------------
// <copyright file="MdModuleController.cs" company="NIC">
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
    /// MdModuleController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdModuleController : ControllerBase
    {
        private readonly IMdModuleDirector mdmoduleDirector;
        private readonly ILogger<MdModuleController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdModuleController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdmoduleDirector">mdmoduleDirector.</param>
        /// <param name="logger">logger.</param>
        public MdModuleController(IMdModuleDirector mdmoduleDirector, ILogger<MdModuleController> logger)
        {
            this.mdmoduleDirector = mdmoduleDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdModule List.
        /// </summary>
        /// <returns>Get All MdModule List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdModule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdModule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdModule>>> GetAsync()
        {
            return await mdmoduleDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdModule List By Id.
        /// </summary>
        /// <param name="ModuleId">ModuleId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdModule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdModule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdModule>> GetAsync(string ModuleId)
        {
            var response = await mdmoduleDirector.GetByIdAsync(ModuleId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdModule.
        /// </summary>
        /// <param name="mdmodule">mdmodule.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdModule mdmodule)
        {
            if (mdmodule == null)
            {
                return BadRequest(mdmodule);
            }

            try
            {
                var response = await mdmoduleDirector.InsertAsync(mdmodule, default).ConfigureAwait(false);
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
        /// Update to MdModule.
        /// </summary>
        /// <param name="mdmodule">mdmodule.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdModule mdmodule)
        {
            if (mdmodule == null)
            {
                return BadRequest(nameof(mdmodule));
            }

            if (mdmodule.ModuleId == "0")
            {
                return BadRequest(nameof(mdmodule.ModuleId));
            }

            try
            {
				var response = await mdmoduleDirector.UpdateAsync(mdmodule, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdModule.
        /// </summary>
        /// <param name="ModuleId">ModuleId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string ModuleId)
        {
            string status;
            var response = await mdmoduleDirector.DeleteAsync(ModuleId, default).ConfigureAwait(false);

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
	