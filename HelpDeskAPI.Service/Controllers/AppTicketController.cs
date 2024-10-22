
//-----------------------------------------------------------------------
// <copyright file="AppTicketController.cs" company="NIC">
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
    /// AppTicketController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppTicketController : ControllerBase
    {
        private readonly IAppTicketDirector appticketDirector;
        private readonly ILogger<AppTicketController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppTicketController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appticketDirector">appticketDirector.</param>
        /// <param name="logger">logger.</param>
        public AppTicketController(IAppTicketDirector appticketDirector, ILogger<AppTicketController> logger)
        {
            this.appticketDirector = appticketDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppTicket List.
        /// </summary>
        /// <returns>Get All AppTicket List.</returns>        
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppTicket), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppTicket), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAllByRole")]
        public async Task<ActionResult<List<AbsModels.GetTicketListByID>>> GetAsync(GetTicketByUserAndStatus userIdAndStatus)
        {var response= await appticketDirector.GetAllAsync(userIdAndStatus, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Get AppTicket List By Id.
        /// </summary>
        /// <param name="TicketId">TicketId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppTicket), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppTicket), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<List<AbsModels.GetTicketListByID>>> GetAsync(int TicketId)
        {
            var response = await appticketDirector.GetByIdAsync(TicketId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert AppTicket.
        /// </summary>
        /// <param name="appticket">appticket.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.GenerateTicket appticket)
        {
            string status;
            if (appticket == null)
            {
                return BadRequest(appticket);
            }

            try
            {
                var response = await appticketDirector.InsertAsync(appticket, default).ConfigureAwait(false);
                if (response ==true)
                {
                    status = "\"Save Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }

                return response == true ? Created(string.Empty, status) : Ok(status);
               // return response == true ? Created(string.Empty, response) : Ok(response);
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
        /// Update to AppTicket.
        /// </summary>
        /// <param name="appticket">appticket.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult<string>> PutAsync(AbsModels.AppTicket appticket)
        {
            string status;
            if (appticket == null)
            {
                return BadRequest(nameof(appticket));
            }

            if (appticket.TicketId == 0)
            {
                return BadRequest(nameof(appticket.TicketId));
            }

            try
            {
				var response = await appticketDirector.UpdateAsync(appticket, default).ConfigureAwait(false);
                if (response > 0)
                {
                    status = "\"Update Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }

                return response > 0 ? Created(string.Empty, status) : Ok(status);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete AppTicket.
        /// </summary>
        /// <param name="TicketId">TicketId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int TicketId)
        {
            string status;
            var response = await appticketDirector.DeleteAsync(TicketId, default).ConfigureAwait(false);

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
        /// <summary>
        /// Get AppTicket List By Id.
        /// </summary>
        /// <param name="TicketId">TicketId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppTicket), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppTicket), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByBoard")]
        public async Task<ActionResult<List<AbsModels.AppTicket>>> GetByBoardAsync(GetTicketbyService getTicketByService)
        {
            var response = await appticketDirector.GetByBoardAsync(getTicketByService, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
    }
	}
	