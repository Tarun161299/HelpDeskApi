using Azure;
using Azure.Core;
using HelpDeskAPI.Data.Abstractions.Behaviors;
using HelpDeskAPI.Data.Abstractions.Models;
using HelpDeskAPI.Data.Business.Behaviors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using AbsModels = HelpDeskAPI.Data.Abstractions.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using HelpDeskAPI.Data.Business.Services;

namespace HelpDeskAPI.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationDirector IJwtAuthenticationDirector;
        private readonly EncryptionDecryptionService EncryptionDecryptionService;

        public JwtAuthenticationController(IJwtAuthenticationDirector _IJwtAuthenticationDirector, EncryptionDecryptionService _EncryptionDecryptionService)
        {
            IJwtAuthenticationDirector = _IJwtAuthenticationDirector;
            EncryptionDecryptionService = _EncryptionDecryptionService;
        }

        //[AllowAnonymous]
        [HttpPost("Authorize")]
        public async  Task<ActionResult<Token>> AuthUser([FromBody] AbsModels.UserInfo user) 
        {
            //string decryptedUserId = Encoding.UTF8.GetString(Convert.FromBase64String(user.Username));
            string decryptedUserId = EncryptionDecryptionService.Decryption(user.Username);
            var token =await IJwtAuthenticationDirector.Authenticate(decryptedUserId, user.Role,user.Mode,default).ConfigureAwait(false); 
            
            return Ok(token);
        }

        [HttpGet("RefreshToken")]
        public async Task<ActionResult<UserAuthorization>> RefreshToken()
        {
            var tokentemp = new Token();
            string message="";
            string token = Request.Headers["Authorization"];
            string refreshToken = Request.Headers["RefreshToken"];
            var stream = token.Substring(7);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var userName = tokenS.Claims.First(claim => claim.Type == "UserID").Value;
            var mode = tokenS.Claims.First(claim => claim.Type == "mode").Value;
            var role=  tokenS.Claims.First(claim => claim.Type == "Role").Value;
            Encoding unicode = Encoding.Unicode;
            //string decryptedUserId = Encoding.UTF8.GetString(Convert.FromBase64String(token.Username));
            var result = await IJwtAuthenticationDirector.RefreshToken(token, refreshToken, userName, role, mode, default).ConfigureAwait(false);
            //Response.Headers.Add("newRefreshToken", result.RefreshToken);
            //Response.Headers.Add("newToken", result.CreatedToken);
            //tokentemp = result;
            //message = "Token Refresh Successfully";
            return result;
        }
    }
}


