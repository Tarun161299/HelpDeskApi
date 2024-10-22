
//-----------------------------------------------------------------------
// <copyright file="AppRemarksController.cs" company="NIC">
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
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// AppRemarksController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppRemarksController : ControllerBase
    {
        private readonly IAppRemarksDirector appremarksDirector;
        private readonly ILogger<AppRemarksController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRemarksController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appremarksDirector">appremarksDirector.</param>
        /// <param name="logger">logger.</param>
        public AppRemarksController(IAppRemarksDirector appremarksDirector, ILogger<AppRemarksController> logger)
        {
            this.appremarksDirector = appremarksDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppRemarks List.
        /// </summary>
        /// <returns>Get All AppRemarks List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppRemarks), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppRemarks), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.AppRemarks>>> GetAsync()
        {
            return await appremarksDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get AppRemarks List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppRemarks), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppRemarks), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.AppRemarks>> GetAsync(int Id)
        {
            var response = await appremarksDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get AppRemarks List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppRemarks), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppRemarks), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByModuleId")]
        public async Task<ActionResult<List<AbsModels.AppRemarks>>> GetbyModuleIdAsync(AppRemarksData appRemarksData)
        {
            var response = await appremarksDirector.GetByModuleIdAsync(appRemarksData, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert AppRemarks.
        /// </summary>
        /// <param name="appremarks">appremarks.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult<string>> PostAsync([FromBody] AbsModels.AppRemarks appremarks)
        {
            
            if (appremarks == null)
            {
                return BadRequest(appremarks);
            }

            try
            {
                var response = await appremarksDirector.InsertAsync(appremarks, default).ConfigureAwait(false);
                string status;
                if (response > 0)
                {
                    return status = "\"Stored Successfully\"";
                }
                else
                {
                   return status = "\"Try Again\"";
                }
                //return response > 0 ? Created(string.Empty, response) : Ok(response);
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
        /// Update to AppRemarks.
        /// </summary>
        /// <param name="appremarks">appremarks.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.AppRemarks appremarks)
        {
            if (appremarks == null)
            {
                return BadRequest(nameof(appremarks));
            }

            if (appremarks.Id == 0)
            {
                return BadRequest(nameof(appremarks.Id));
            }

            try
            {
				var response = await appremarksDirector.UpdateAsync(appremarks, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete AppRemarks.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int Id)
        {
            string status;
            var response = await appremarksDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	