
//-----------------------------------------------------------------------
// <copyright file="MdSectionController.cs" company="NIC">
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
    /// MdSectionController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdSectionController : ControllerBase
    {
        private readonly IMdSectionDirector mdsectionDirector;
        private readonly ILogger<MdSectionController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdSectionController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mdsectionDirector">mdsectionDirector.</param>
        /// <param name="logger">logger.</param>
        public MdSectionController(IMdSectionDirector mdsectionDirector, ILogger<MdSectionController> logger)
        {
            this.mdsectionDirector = mdsectionDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdSection List.
        /// </summary>
        /// <returns>Get All MdSection List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdSection), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdSection), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdSection>>> GetAsync()
        {
            return await mdsectionDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdSection List By Id.
        /// </summary>
        /// <param name="SectionId">SectionId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdSection), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdSection), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdSection>> GetAsync(int SectionId)
        {
            var response = await mdsectionDirector.GetByIdAsync(SectionId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdSection.
        /// </summary>
        /// <param name="mdsection">mdsection.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdSection mdsection)
        {
            if (mdsection == null)
            {
                return BadRequest(mdsection);
            }

            try
            {
                var response = await mdsectionDirector.InsertAsync(mdsection, default).ConfigureAwait(false);
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
        /// Update to MdSection.
        /// </summary>
        /// <param name="mdsection">mdsection.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdSection mdsection)
        {
            if (mdsection == null)
            {
                return BadRequest(nameof(mdsection));
            }

            if (mdsection.SectionId == 0)
            {
                return BadRequest(nameof(mdsection.SectionId));
            }

            try
            {
				var response = await mdsectionDirector.UpdateAsync(mdsection, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdSection.
        /// </summary>
        /// <param name="SectionId">SectionId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int SectionId)
        {
            string status;
            var response = await mdsectionDirector.DeleteAsync(SectionId, default).ConfigureAwait(false);

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
	