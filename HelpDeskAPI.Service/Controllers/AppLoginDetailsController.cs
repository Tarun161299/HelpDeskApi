
//-----------------------------------------------------------------------
// <copyright file="AppLoginDetailsController.cs" company="NIC">
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
    using System.Text.Json;

    /// <summary>
    /// AppLoginDetailsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppLoginDetailsController : ControllerBase
    {
        private readonly IAppLoginDetailsDirector applogindetailsDirector;
        private readonly ILogger<AppLoginDetailsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppLoginDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="applogindetailsDirector">applogindetailsDirector.</param>
        /// <param name="logger">logger.</param>
        public AppLoginDetailsController(IAppLoginDetailsDirector applogindetailsDirector, ILogger<AppLoginDetailsController> logger)
        {
            this.applogindetailsDirector = applogindetailsDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppLoginDetails List.
        /// </summary>
        /// <returns>Get All AppLoginDetails List.</returns>        
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.AppLoginDetails>>> GetAsync()
        {
            return await applogindetailsDirector.GetAllAsync(default).ConfigureAwait(false);
        }

		/// <summary>
        /// Get AppLoginDetails List By Id.
        /// </summary>
        /// <param name="UserId">UserId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.AppLoginDetails>> GetAsync(string UserId)
        {
            var response = await applogindetailsDirector.GetByIdAsync(UserId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Get AppLoginDetails List By Id.
        /// </summary>
        /// <param name="UserId">UserId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppLoginDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByEisId")]
        public async Task<ActionResult<AbsModels.AppLoginDetails>> GetByEisIdAsync(string eisId)
        {
            var response = await applogindetailsDirector.GetByEisIdAsync(eisId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetCaptcha")]
        public async Task<ActionResult<string>> GetCaptcha()
        {
            var response = await applogindetailsDirector.GetCaptcha(default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }
        /// <summary>
        /// Insert AppLoginDetails.
        /// </summary>
        /// <param name="applogindetails">applogindetails.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.AppLoginDetails applogindetails)
        {
            if (applogindetails == null)
            {
                return BadRequest(applogindetails);
            }

            try
            {
                var response = await applogindetailsDirector.InsertAsync(applogindetails, default).ConfigureAwait(false);
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
        /// Update to AppLoginDetails.
        /// </summary>
        /// <param name="applogindetails">applogindetails.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.AppLoginDetails applogindetails)
        {
            if (applogindetails == null)
            {
                return BadRequest(nameof(applogindetails));
            }

            if (applogindetails.UserId == "0")
            {
                return BadRequest(nameof(applogindetails.UserId));
            }

            try
            {
				var response = await applogindetailsDirector.UpdateAsync(applogindetails, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

		/// <summary>
        /// Delete AppLoginDetails.
        /// </summary>
        /// <param name="UserId">UserId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(string UserId)
        {
            string status;
            var response = await applogindetailsDirector.DeleteAsync(UserId, default).ConfigureAwait(false);

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
        /// Check userID Availibility.
        /// </summary>
        /// <param name="userID">Check UserID Availibility.</param>
        /// <returns>get by UserID.</returns>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [Route("CheckUserIDAvailibility")]
        public async Task<ActionResult<string>> CheckUserIdAvailibity(string userID)
        {
            var response = await applogindetailsDirector.CheckUserIdAvailibity(userID, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Save SignUp Detail.
        /// </summary>
        /// <param name="signUp">signUp.</param>
        /// <returns>SignUp.</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("SaveSignUp")]
        public async Task<ActionResult<string>> PostSignUpAsync(SignUp signUp)
        {
            var response = await applogindetailsDirector.SaveSignUpDetailsAsync(signUp, default).ConfigureAwait(false);
            string status;
            switch (response)
            {
                case 1: status = "\"Data Stored Successfully\""; break;
                case 2: status = "\"Data Stored Successfully\""; break;
                default: status = "\"Try Again\""; break;
            }

            return response > 0 ? Created(string.Empty, status) : Ok(status);
        }
    }
	}
	