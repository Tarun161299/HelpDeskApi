
//-----------------------------------------------------------------------
// <copyright file="AppServiceRequestController.cs" company="NIC">
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
    using System.Security.Cryptography;

    /// <summary>
    /// AppServiceRequestController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppServiceRequestController : ControllerBase
    {
        private readonly IAppServiceRequestDirector appservicerequestDirector;
        private readonly ILogger<AppServiceRequestController> logger;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceRequestController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appservicerequestDirector">appservicerequestDirector.</param>
        /// <param name="logger">logger.</param>
        public AppServiceRequestController(IAppServiceRequestDirector appservicerequestDirector, ILogger<AppServiceRequestController> logger)
        {
            this.appservicerequestDirector = appservicerequestDirector;
            this.logger = logger;
            
        }

        /// <summary>
        /// Get AppServiceRequest List.
        /// </summary>
        /// <returns>Get All AppServiceRequest List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AppServiceRequestsList>>> GetAsync()
        {
            return await appservicerequestDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Insert in AppServiceRequest.
        /// </summary>
        /// <param name="ServiceUserId">ServiceRequestId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.AppServiceRequest>> GetAsync(GetServiceByUserAndStatus Userdetails)
        {
            var response = await appservicerequestDirector.GetByIdAsync(Userdetails, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert in AppServiceRequest.
        /// </summary>
        /// <param name="ServiceRequestId">ServiceRequestId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByRequestId")]
        public async Task<ActionResult<List<AbsModels.AppServiceRequestsList>>> GetByRequestIdAsync(string serviceRequestId)
        {
            var response = await appservicerequestDirector.GetByRequestIdAsync(serviceRequestId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert AppServiceRequest.
        /// </summary>
        /// <param name="appservicerequest">appservicerequest.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult<string>> PostAsync([FromBody] AbsModels.AppServiceRequest appservicerequest)
            {
            if (appservicerequest == null)
            {
                return BadRequest(appservicerequest);
            }

            try
            {
                var response = await appservicerequestDirector.InsertAsync(appservicerequest, default).ConfigureAwait(false);
                string status;
                if(response>0)
                {
                    return status = "\"Stored Successfully\"";
                }
                else
                {
                    return status = "\"Try Again\"";
                }
     
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
        /// Update to AppServiceRequest.
        /// </summary>
        /// <param name="appservicerequest">appservicerequest.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult<string>> PutAsync(AbsModels.AppServiceRequest appservicerequest)
        {
            if (appservicerequest == null)
            {
                return BadRequest(nameof(appservicerequest));
            }

            if (appservicerequest.ServiceRequestNo == "")
            {
                return BadRequest(nameof(appservicerequest.ServiceRequestNo));
            }

            try
            {
                string status;
              
                    var response = await appservicerequestDirector.UpdateAsync(appservicerequest, default).ConfigureAwait(false);
                    
                    if (response > 0 && response!=999)
                    {
                        return status = "\"Updated Successfully\"";
                    }
                    if(response == 999)
                    {
                    return status= "\"Can't update Status To closed Some Tickets Are Running\"";
                    }
                    else
                    {
                        return status = "\"Try Again\"";
                    }
                
                //else if(appservicerequest.Status == "SA")
                //{

                //    return status = "\"Request is Already Assigned\"";
                //}
                //else if (appservicerequest.Status == "SR")
                //{
                //    return status = "\"Request is Rejected\"";
                //}
                //else
                //{
                //    return status = "\"Try Again\"";
                //}
               
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete AppServiceRequest.
        /// </summary>
        /// <param name="ServiceRequestId">ServiceRequestId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<int>> DeleteAsync(int ServiceRequestId)
        {
            string status;
                var response = await appservicerequestDirector.DeleteAsync(ServiceRequestId, default).ConfigureAwait(false);

            //if (response > 0)
            //{
            //    status = "\"Delete Successfully\"";
            //}
            //else
            //{
            //    status = "\"Try Again\"";
            //}

            return response ;
        }
        /// <summary>
        /// Insert in AppServiceRequest.
        /// </summary>
        /// <param name="ServiceRequestId">ServiceRequestId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppServiceRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByIdData")]
        public async Task<ActionResult<List<AbsModels.AppServiceRequestsList>>> GetDataAsync(getserviceRequest appServiceRequest)
        {
            var response = await appservicerequestDirector.GetByIdAsync(appServiceRequest, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }


        /// <summary>
        /// GetStatus Count.
        /// </summary>
        /// <returns>get by id.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DashboardCount), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DashboardCount), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetStatusCountAsync")]
        public async Task<ActionResult<DashboardCount>> GetStatusCountAsync(StatusCount statusCount)
        {
            var response = await appservicerequestDirector.GetStatusCountAsync(statusCount, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Save AppOnboarding Request Data.
        /// </summary>
        /// <param name="appOnboardingRequest">appOnboardingRequest.</param>
        /// <returns>AppOnboardingRequest.</returns>

      
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppServiceRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.AppServiceRequest), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("OTP")]
        public async Task<ActionResult<string>> PostOTPAsync(OTPModal otpModal)
        {
            var response = await appservicerequestDirector.SendOTP(otpModal, default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SignUp), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetIP")]
        public async Task<ActionResult<string>> GetIPAddress()
        {
            var response = await appservicerequestDirector.GetIPAddress(default).ConfigureAwait(false);
            return JsonSerializer.Serialize(response);
        }



    }

}
