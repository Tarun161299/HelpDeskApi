using HelpDeskAPI.Data.Abstractions.Behaviors;
using HelpDeskAPI.Data.Abstractions.Models;
using HelpDeskAPI.Data.Business.Behaviors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Web;

namespace HelpDeskAPI.Service.Controllers
{
    [Route("api/[controller]")]
    public class ExtEndPointController : Controller
    {
        private readonly IExtEndPointDirector extEndPointDirector;
        private readonly ILogger<AppServiceRequestController> logger;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstProjectsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstprojectsDirector">zmstprojectsDirector.</param>
        /// <param name="logger">logger.</param>
        public ExtEndPointController(IExtEndPointDirector extEndPointDirector, ILogger<ExtEndPointController> logger, IConfiguration configuration)
        {
            this.extEndPointDirector = extEndPointDirector;
            _config = configuration;
        }
        //[HttpPost]
        //[Route("")]
        //public async Task<ActionResult> Index(string access_token, string expires_in, string refresh_token, string token_type, string token_id,string state,string nonce)
        //{
        //    var jsonToken = access_token;
        //    var cliamdata = Decode(token_id);
        //    //UserProfile user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(cliamdata);
        //    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(cliamdata);
        //    var response = await extEndPointDirector.InsertAsync(access_token, expires_in, refresh_token, token_type, token_id, cliamdata, default).ConfigureAwait(false);
        //    string redirectUrl = _config.GetSection("ClientAppRedirectUri").Value.ToString();
        //    return new RedirectResult(redirectUrl + response);
        //}

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Index(string access_token, string expires_in, string token_type, string token_id, string state, string nonce)
        {
            var jsonToken = access_token;
            var cliamdata = Decode(token_id);
            //UserProfile user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(cliamdata);
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(cliamdata);
            var response = await extEndPointDirector.InsertAsync(access_token, expires_in, string.Empty, token_type, token_id, cliamdata, default).ConfigureAwait(false);
            string redirectUrl = _config.GetSection("ClientAppRedirectUri").Value.ToString();
            return new RedirectResult(redirectUrl + response);
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("mobileapp/authresponse")]
        public async Task<ActionResult> AndroidAuthResponse(string access_token, string expires_in, string token_type, string token_id)
        {
            try
            {
                var jsonToken = access_token;
                var cliamdata = Decode(token_id);
                //UserProfile user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserProfile>(cliamdata);
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(cliamdata);

                //return "Hello" + user;
                var response = await extEndPointDirector.InsertAsync(access_token, expires_in, string.Empty, token_type, token_id, cliamdata, default).ConfigureAwait(false);
                string redirectUrl = _config.GetSection("AndroidAppRedirectUri").Value.ToString();
                return new RedirectResult(redirectUrl + response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("mobileapp/authresponse")]
        public IActionResult AndroidAuthResponse()
        {
            return View();
        }

        
        [HttpPost]
        [Route("Logout")]
        public async Task<ActionResult> LogoutEis([FromBody] Logout logout) 
        {
            try
            {
                var response = await extEndPointDirector.LogoutAsync(logout, default).ConfigureAwait(false);
                return new RedirectResult(response.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }

        /// <summary>
        /// Get AppDocumentUploadedDetail List By Id.
        /// </summary>
        /// <param name="DocumentId">DocumentId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Esodata), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Esodata), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetById")]
        public async Task<ActionResult<Esodata>> GetAsync(long id)
        {
            var response = await extEndPointDirector.GetByIdAsync(id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }


    }
}

