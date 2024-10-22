
//-----------------------------------------------------------------------
// <copyright file="ZmstProjectsController.cs" company="NIC">
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
    /// ZmstProjectsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ZmstProjectsController : ControllerBase
    {
        private readonly IZmstProjectsDirector zmstprojectsDirector;
        private readonly ILogger<ZmstProjectsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstProjectsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstprojectsDirector">zmstprojectsDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstProjectsController(IZmstProjectsDirector zmstprojectsDirector, ILogger<ZmstProjectsController> logger)
        {
            this.zmstprojectsDirector = zmstprojectsDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstProjects List.
        /// </summary>
        /// <returns>Get All ZmstProjects List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstProjects), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstProjects), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.ZmstProjects>>> GetAsync()
        {
            return await zmstprojectsDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstProjects List.
        /// </summary>
        /// <returns>Get All ZmstProjects List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstProjects), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstProjects), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetbyUserId")]
        public async Task<ActionResult<List<AbsModels.ZmstProjects>>> GetByUserIdAsync(string userId)
        {
            return await zmstprojectsDirector.GetByUserIdAsync(userId,default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get ZmstProjects List By Id.
        /// </summary>
        /// <param name="AgencyId">AgencyId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstProjects), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstProjects), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.ZmstProjects>> GetAsync(int AgencyId)
        {
            var response = await zmstprojectsDirector.GetByIdAsync(AgencyId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert ZmstProjects.
        /// </summary>
        /// <param name="zmstprojects">zmstprojects.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.ZmstProjects zmstprojects)
        {
            if (zmstprojects == null)
            {
                return BadRequest(zmstprojects);
            }

            try
            {
                var response = await zmstprojectsDirector.InsertAsync(zmstprojects, default).ConfigureAwait(false);
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
        /// Update to ZmstProjects.
        /// </summary>
        /// <param name="zmstprojects">zmstprojects.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.ZmstProjects zmstprojects)
        {
            if (zmstprojects == null)
            {
                return BadRequest(nameof(zmstprojects));
            }

            if (zmstprojects.AgencyId == 0)
            {
                return BadRequest(nameof(zmstprojects.AgencyId));
            }

            try
            {
				var response = await zmstprojectsDirector.UpdateAsync(zmstprojects, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete ZmstProjects.
        /// </summary>
        /// <param name="AgencyId">AgencyId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int AgencyId)
        {
            string status;
            var response = await zmstprojectsDirector.DeleteAsync(AgencyId, default).ConfigureAwait(false);

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
	