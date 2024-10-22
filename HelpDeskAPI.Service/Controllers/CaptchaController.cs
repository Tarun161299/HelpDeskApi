using Microsoft.AspNetCore.Mvc;
using SixLaborsCaptcha.Core;
using HelpDeskAPI.Data.Abstractions.Behaviors;
using Extensions = SixLaborsCaptcha.Core.Extensions;
using AbsModels = HelpDeskAPI.Data.Abstractions.Models;
using HelpDeskAPI.Data.Abstractions.Models;

namespace HelpDeskAPI.Service.Controllers
{
	[Route("api/[controller]")]
	public class CaptchaController : Controller
	{
		private readonly ICaptchaDirector ecaptchaDirector;
		private readonly ILogger<CaptchaController> logger;
		private readonly IConfiguration _config;
		/// <summary>
		/// Initializes a new instance of the <see cref="CaptchaController"/> class.
		/// Constructor.
		/// </summary>
		/// <param name="CaptchaController">CaptchaController.</param>
		/// <param name="logger">logger.</param>
		public CaptchaController(ICaptchaDirector captchaDirector, ILogger<CaptchaController> logger, IConfiguration configuration)
		{
			this.ecaptchaDirector = captchaDirector;
			_config = configuration;
		}

		[HttpGet]
		[Route("")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<ActionResult<AbsModels.AppCaptcha>> GetCaptchaImage([FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha)

		{
			string hashvalue;
		    string key = Extensions.GetUniqueKey(6);
			//string key = "123456";
            var imgText = sixLaborsCaptcha.Generate(key);
			string base64String = Convert.ToBase64String(imgText, 0, imgText.Length);
			// Use input string to calculate MD5 hash
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
				byte[] hashBytes = md5.ComputeHash(inputBytes);
				hashvalue = Convert.ToHexString(hashBytes);
			}

			var response = await ecaptchaDirector.InsertAsync(key, base64String, hashvalue, default).ConfigureAwait(false);
			#region(Vimal "You can check captcha in View" url: http://localhost:5197/Captcha)
			//byte[] imageBytes = Convert.FromBase64String(response.CaptchBaseString);
			//return File(imageBytes, "Image/Png");
			#endregion(Vimal)
			return response == null ? Created(string.Empty, response) : Ok(response);

		}

        [HttpPost]
        [Route("CheckCaptcha")]
        public async Task<ActionResult<int>> CheckCaptcha([FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha,string input)

        {
            string hashvalue;
            string key = input;
            var imgText = sixLaborsCaptcha.Generate(key);
            string base64String = Convert.ToBase64String(imgText, 0, imgText.Length);
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                hashvalue = Convert.ToHexString(hashBytes);
            }
            Check_captcha captcha= new Check_captcha();
            captcha.key= key;
            captcha.hash= hashvalue;
            var response = await ecaptchaDirector.checkCaptcha(captcha, default).ConfigureAwait(false);
            return response;
        }
    }
}
