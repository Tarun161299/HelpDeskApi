//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace HelpDeskAPI.Service.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AgencyController : ControllerBase
//    {
//    }
//}

//-----------------------------------------------------------------------
// <copyright file="AgencyController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Service.Controllers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using HelpDeskAPI.Data.Abstractions.Behaviors;
    using AbsModels = HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// AppOnboardingDetailsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IMdAgencyDirector iagency;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppOnboardingdetail">iAppOnboardingdetail.</param>

        public AgencyController(IMdAgencyDirector _iagency)
        {
            this.iagency = _iagency;
        }

        /// <summary>
        /// Agency List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdAgency>>> GetAsync()
        {
            return await iagency.GetAllAsync(default).ConfigureAwait(false);
        }
    }
}
