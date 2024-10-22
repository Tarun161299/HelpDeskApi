//-----------------------------------------------------------------------
// <copyright file="JWTTokenService.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace HelpDeskAPI.Data.Business.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using HelpDeskAPI.Data.Abstractions.Models;
    using HelpDeskAPI.Data.Interfaces;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    

    /// <inheritdoc />
    public class JWTTokenService
    {
        // private readonly string key;
        public static UserInfo user = new UserInfo();
       // private readonly JWT _jwtSetting;
        public string RefreshToken;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;
        //private readonly IAppOnboardingAdminloginDirector AppAdminLoginDirector;

        public JWTTokenService( IHttpContextAccessor httpContextAccessor)
        {
            //_jwtSetting = options.Value;
            _httpContextAccessor = httpContextAccessor;

            //this.unitOfWork = _unitOfWork;
        }
        /// <inheritdoc />
        public Token TokenGenerate(string username, string role, string mode)
        {
            var token = new Token();
            var refreshToken = new Token();
            var key = "helpDeskSystemkey21072023";
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                 {
                      new Claim("UserID", username),
                      new Claim("Role", role),
                      new Claim("mode", mode),
                 }),
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
            token.CreatedToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            token.TokenExpires = DateTime.UtcNow.AddDays(15);
            token.TokenCreated = DateTime.Now;

            refreshToken = GenerateRefreshToken(token);
            return token;
        }

        private Token GenerateRefreshToken(Token token)
        {

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            token.RefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            token.RefreshTokenExpires = DateTime.UtcNow.AddHours(1);
            token.RefreshTokenCreated = DateTime.Now;
            RefreshToken = token.RefreshToken;
           
            return token;
        }

        

        public void RemoveToken()
        {
            RefreshToken = "";
        }

        public string GetPrincipalFromExpiredToken(string? tokens)
        {
            var key = "helpDeskSystemkey21072023";
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = new Token();
            var refreshToken = new Token();
            JwtSecurityTokenHandler tokenHandlers = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var stream = "[encoded jwt]";
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(tokens);
            var tokenS = jsonToken as JwtSecurityToken;
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                      {
                      new Claim(ClaimTypes.Name,tokenS.Claims.ToString()),

                      }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };
            token.CreatedToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            token.TokenExpires = DateTime.UtcNow.AddMilliseconds(1);
            token.TokenCreated = DateTime.Now;
            refreshToken = GenerateRefreshToken(token);
            return token.CreatedToken;

        }

        //public virtual async Task<string> Token(string token, string username, CancellationToken cancellationToken)
        //{
        //    var appRefreshToken = await this.unitOfWork.AppLoginDetailRepository.FindAllByAsync(x => x.UserToken == token && x.UserName == username, cancellationToken).ConfigureAwait(false);
        //    return appRefreshToken.ToString();
        //}
    }
}

