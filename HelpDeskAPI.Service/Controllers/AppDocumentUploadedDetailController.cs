
//-----------------------------------------------------------------------
// <copyright file="AppDocumentUploadedDetailController.cs" company="NIC">
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
    /// AppDocumentUploadedDetailController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppDocumentUploadedDetailController : ControllerBase
    {
        private readonly IAppDocumentUploadedDetailDirector appdocumentuploadeddetailDirector;
        private readonly ILogger<AppDocumentUploadedDetailController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDocumentUploadedDetailController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appdocumentuploadeddetailDirector">appdocumentuploadeddetailDirector.</param>
        /// <param name="logger">logger.</param>
        public AppDocumentUploadedDetailController(IAppDocumentUploadedDetailDirector appdocumentuploadeddetailDirector, ILogger<AppDocumentUploadedDetailController> logger)
        {
            this.appdocumentuploadeddetailDirector = appdocumentuploadeddetailDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppDocumentUploadedDetail List.
        /// </summary>
        /// <returns>Get All AppDocumentUploadedDetail List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.AppDocumentUploadedDetail>>> GetAsync()
        {
            return await appdocumentuploadeddetailDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get AppDocumentUploadedDetail List By Id.
        /// </summary>
        /// <param name="DocumentId">DocumentId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.AppDocumentUploadedDetail>> GetAsync(int fileId)
        {
            var response = await appdocumentuploadeddetailDirector.GetByIdAsync(fileId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert AppDocumentUploadedDetail.
        /// </summary>
        /// <param name="appdocumentuploadeddetail">appdocumentuploadeddetail.</param>
        /// <returns>Effected Row.</returns>
        ///
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult<int>> PostAsync([FromBody] AbsModels.UpdateDocuments appdocumentuploadeddetail)
        {
            //string status;
            if (appdocumentuploadeddetail == null)
            {
                return BadRequest(appdocumentuploadeddetail);
            }

            try
            {
                var response = await appdocumentuploadeddetailDirector.InsertAsync(appdocumentuploadeddetail, default).ConfigureAwait(false);
                //if (response > 0)
                //{
                //    status = "\"insert Successfully\"";
                //}
                //else
                //{
                //    status = "\"Try Again\"";
                //}

                //return response > 0 ? Created(string.Empty, status) : Ok(status);
                return response;
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
        /// Update to AppDocumentUploadedDetail.
        /// </summary>
        /// <param name="appdocumentuploadeddetail">appdocumentuploadeddetail.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.AppDocumentUploadedDetail appdocumentuploadeddetail)
        {
            string status;
            if (appdocumentuploadeddetail == null)
            {
                return BadRequest(nameof(appdocumentuploadeddetail));
            }

            if (appdocumentuploadeddetail.DocumentId == 0)
            {
                return BadRequest(nameof(appdocumentuploadeddetail.DocumentId));
            }

            try
            {
				var response = await appdocumentuploadeddetailDirector.UpdateAsync(appdocumentuploadeddetail, default).ConfigureAwait(false);
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
        /// Delete AppDocumentUploadedDetail.
        /// </summary>
        /// <param name="DocumentId">DocumentId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int DocumentId)
        {
            string status;
            var response = await appdocumentuploadeddetailDirector.DeleteAsync(DocumentId, default).ConfigureAwait(false);

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
	