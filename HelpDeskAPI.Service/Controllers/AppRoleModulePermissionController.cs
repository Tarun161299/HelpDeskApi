
//-----------------------------------------------------------------------
// <copyright file="AppRoleModulePermissionController.cs" company="NIC">
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
    /// AppRoleModulePermissionController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppRoleModulePermissionController : ControllerBase
    {
        private readonly IAppRoleModulePermissionDirector approlemodulepermissionDirector;
        private readonly ILogger<AppRoleModulePermissionController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRoleModulePermissionController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="approlemodulepermissionDirector">approlemodulepermissionDirector.</param>
        /// <param name="logger">logger.</param>
        public AppRoleModulePermissionController(IAppRoleModulePermissionDirector approlemodulepermissionDirector, ILogger<AppRoleModulePermissionController> logger)
        {
            this.approlemodulepermissionDirector = approlemodulepermissionDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppRoleModulePermission List.
        /// </summary>
        /// <returns>Get All AppRoleModulePermission List.</returns>        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppRoleModulePermission), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppRoleModulePermission), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.AppRoleModulePermission>>> GetAsync()
        {
            return await approlemodulepermissionDirector.GetAllAsync(default).ConfigureAwait(false);
        }

        /// <summary>
        /// Get AppRoleModulePermission List By Id.
        /// </summary>
        /// <param name="RoleId">RoleId.</param>
        /// <returns>Get by id.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppRoleModulePermission), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppRoleModulePermission), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<AbsModels.AppRoleModulePermission>> GetAsync(int RoleId)
        {
            var response = await approlemodulepermissionDirector.GetByIdAsync(RoleId, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }

        /// <summary>
        /// Insert AppRoleModulePermission.
        /// </summary>
        /// <param name="approlemodulepermission">approlemodulepermission.</param>
        /// <returns>Effected Row.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("Insert")]
        public async Task<ActionResult> PostAsync([FromBody] AbsModels.AppRoleModulePermission approlemodulepermission)
        {
            if (approlemodulepermission == null)
            {
                return BadRequest(approlemodulepermission);
            }

            try
            {
                var response = await approlemodulepermissionDirector.InsertAsync(approlemodulepermission, default).ConfigureAwait(false);
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
        /// Update to AppRoleModulePermission.
        /// </summary>
        /// <param name="approlemodulepermission">approlemodulepermission.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Update")]
		public async Task<ActionResult> PutAsync(AbsModels.AppRoleModulePermission approlemodulepermission)
        {
            if (approlemodulepermission == null)
            {
                return BadRequest(nameof(approlemodulepermission));
            }

            if (approlemodulepermission.RoleId == 0)
            {
                return BadRequest(nameof(approlemodulepermission.RoleId));
            }

            try
            {
				var response = await approlemodulepermissionDirector.UpdateAsync(approlemodulepermission, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
            }
            catch (EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        /// <summary>
        /// Delete AppRoleModulePermission.
        /// </summary>
        /// <param name="RoleId">RoleId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Delete")]
        public async Task<ActionResult<string>> DeleteAsync(int RoleId)
        {
            string status;
            var response = await approlemodulepermissionDirector.DeleteAsync(RoleId, default).ConfigureAwait(false);

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
	