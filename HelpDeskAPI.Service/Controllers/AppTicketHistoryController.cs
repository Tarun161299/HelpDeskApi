
//-----------------------------------------------------------------------
// <copyright file="AppTicketHistoryController.cs" company="NIC">
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
    /// AppTicketHistoryController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppTicketHistoryController : ControllerBase
    {
        private readonly IAppTicketHistoryDirector apptickethistoryDirector;
        private readonly ILogger<AppTicketHistoryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppTicketHistoryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="apptickethistoryDirector">apptickethistoryDirector.</param>
        /// <param name="logger">logger.</param>
        public AppTicketHistoryController(IAppTicketHistoryDirector apptickethistoryDirector, ILogger<AppTicketHistoryController> logger)
        {
            this.apptickethistoryDirector = apptickethistoryDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppTicketHistory List.
        /// </summary>
        /// <returns>Get All AppTicketHistory List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppTicketHistory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppTicketHistory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.AppTicketHistory>>> GetAsync()
        {
            return await apptickethistoryDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get AppTicketHistory List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppTicketHistory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppTicketHistory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.AppTicketHistory>> GetAsync(int Id)
        {
            var response = await apptickethistoryDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

		/// <summary>
        /// Insert AppTicketHistory.
        /// </summary>
        /// <param name="apptickethistory">apptickethistory.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.AppTicketHistory apptickethistory)
        {
            if (apptickethistory == null)
            {
                return BadRequest(apptickethistory);
            }

            try
            {
                var response = await apptickethistoryDirector.InsertAsync(apptickethistory, default).ConfigureAwait(false);
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
        /// Update to AppTicketHistory.
        /// </summary>
        /// <param name="apptickethistory">apptickethistory.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.AppTicketHistory apptickethistory)
        {
            if (apptickethistory == null)
            {
                return BadRequest(nameof(apptickethistory));
            }

            if (apptickethistory.Id == 0)
            {
                return BadRequest(nameof(apptickethistory.Id));
            }

            try
            {
				var response = await apptickethistoryDirector.UpdateAsync(apptickethistory, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete AppTicketHistory.
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
            var response = await apptickethistoryDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	