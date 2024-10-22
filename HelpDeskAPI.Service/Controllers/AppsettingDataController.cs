using Azure;
using HelpDeskAPI.Data.Abstractions.Behaviors;
using HelpDeskAPI.Data.Business.Behaviors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HelpDeskAPI.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppsettingDataController : ControllerBase
    {

        private readonly IAppSettingDirector iappSettingDirector;
        private readonly ILogger<AppServiceRequestHistoryController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceRequestHistoryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="appservicerequesthistoryDirector">appservicerequesthistoryDirector.</param>
        /// <param name="logger">logger.</param>
        public AppsettingDataController(IAppSettingDirector _iappSettingDirector, ILogger<AppServiceRequestHistoryController> logger)
        {
            this.iappSettingDirector = _iappSettingDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get AppServiceRequestHistory List.
        /// </summary>
        /// <returns>Get All AppServiceRequestHistory List.</returns>  
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetHeaders")]
        public async Task<ActionResult<string>> GetHeaders()
        {
           var response= await this.iappSettingDirector.GetHeaders(default).ConfigureAwait(false); ;
           return JsonSerializer.Serialize(response);
        }

        /// <summary>
        /// Get AppServiceRequestHistory List.
        /// </summary>
        /// <returns>Get All AppServiceRequestHistory List.</returns>  
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetResolutionDate")]
        public async Task<ActionResult<int>> GetResolutionDate(string priority)
        {
            var response = await this.iappSettingDirector.GetResolutionDate(priority,default).ConfigureAwait(false); ;
            return response;
        }
    }
}
