
//-----------------------------------------------------------------------
// <copyright file="AppServiceRequestHistoryController.cs" company="NIC">
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
    /// AppServiceRequestHistoryController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppServiceRequestHistoryController : ControllerBase
    {
        private readonly IAppServiceRequestHistoryDirector appservicerequesthistoryDirector;
        private readonly ILogger<AppServiceRequestHistoryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceRequestHistoryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appservicerequesthistoryDirector">appservicerequesthistoryDirector.</param>
        /// <param name="logger">logger.</param>
        public AppServiceRequestHistoryController(IAppServiceRequestHistoryDirector appservicerequesthistoryDirector, ILogger<AppServiceRequestHistoryController> logger)
        {
            this.appservicerequesthistoryDirector = appservicerequesthistoryDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppServiceRequestHistory List.
        /// </summary>
        /// <returns>Get All AppServiceRequestHistory List.</returns>  
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequestHistory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequestHistory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.AppServiceRequestHistory>>> GetAsync()
        {
            return await appservicerequesthistoryDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get AppServiceRequestHistory List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequestHistory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequestHistory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.AppServiceRequestHistory>> GetAsync(int Id)
        {
            var response = await appservicerequesthistoryDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert AppServiceRequestHistory.
        /// </summary>
        /// <param name="appservicerequesthistory">appservicerequesthistory.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.AppServiceRequestHistory appservicerequesthistory)
        {
            if (appservicerequesthistory == null)
            {
                return BadRequest(appservicerequesthistory);
            }

            try
            {
                var response = await appservicerequesthistoryDirector.InsertAsync(appservicerequesthistory, default).ConfigureAwait(false);
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
        /// Update to AppServiceRequestHistory.
        /// </summary>
        /// <param name="appservicerequesthistory">appservicerequesthistory.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.AppServiceRequestHistory appservicerequesthistory)
        {
            if (appservicerequesthistory == null)
            {
                return BadRequest(nameof(appservicerequesthistory));
            }

            if (appservicerequesthistory.Id == 0)
            {
                return BadRequest(nameof(appservicerequesthistory.Id));
            }

            try
            {
				var response = await appservicerequesthistoryDirector.UpdateAsync(appservicerequesthistory, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete AppServiceRequestHistory.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
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
            var response = await appservicerequesthistoryDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	