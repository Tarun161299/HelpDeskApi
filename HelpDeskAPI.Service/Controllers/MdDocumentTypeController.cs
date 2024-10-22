
//-----------------------------------------------------------------------
// <copyright file="MdDocumentTypeController.cs" company="NIC">
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
    /// MdDocumentTypeController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MdDocumentTypeController : ControllerBase
    {
        private readonly IMdDocumentTypeDirector mddocumenttypeDirector;
        private readonly ILogger<MdDocumentTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdDocumentTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="mddocumenttypeDirector">mddocumenttypeDirector.</param>
        /// <param name="logger">logger.</param>
        public MdDocumentTypeController(IMdDocumentTypeDirector mddocumenttypeDirector, ILogger<MdDocumentTypeController> logger)
        {
            this.mddocumenttypeDirector = mddocumenttypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get MdDocumentType List.
        /// </summary>
        /// <returns>Get All MdDocumentType List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdDocumentType>>> GetAsync()
        {
            return await mddocumenttypeDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get MdDocumentType List By Id.
        /// </summary>
        /// <param name="Id">Id.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.MdDocumentType>> GetAsync(string Id)
        {
            var response = await mddocumenttypeDirector.GetByIdAsync(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert MdDocumentType.
        /// </summary>
        /// <param name="mddocumenttype">mddocumenttype.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.MdDocumentType mddocumenttype)
        {
            if (mddocumenttype == null)
            {
                return BadRequest(mddocumenttype);
            }

            try
            {
                var response = await mddocumenttypeDirector.InsertAsync(mddocumenttype, default).ConfigureAwait(false);
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
        /// Update to MdDocumentType.
        /// </summary>
        /// <param name="mddocumenttype">mddocumenttype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.MdDocumentType mddocumenttype)
        {
            if (mddocumenttype == null)
            {
                return BadRequest(nameof(mddocumenttype));
            }

            if (mddocumenttype.Id == "0")
            {
                return BadRequest(nameof(mddocumenttype.Id));
            }

            try
            {
				var response = await mddocumenttypeDirector.UpdateAsync(mddocumenttype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete MdDocumentType.
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
        public async Task<ActionResult<string>> DeleteAsync(string Id)
        {
            string status;
            var response = await mddocumenttypeDirector.DeleteAsync(Id, default).ConfigureAwait(false);

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
	