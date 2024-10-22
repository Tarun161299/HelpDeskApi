using HelpDeskAPI.Data.Abstractions.Behaviors;
using Microsoft.IdentityModel.Tokens;
using HelpDeskAPI.Data.Business.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HelpDeskAPI.Data.Abstractions.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using HelpDeskAPI.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HelpDeskAPI.Data.EF.Models;
using Azure.Core;
using AutoMapper;

namespace HelpDeskAPI.Data.Business.Behaviors
{
    public class JwtAuthenticationDirector : IJwtAuthenticationDirector
    {
        //private readonly string key;
        private readonly JWTTokenService jwtTokenServices;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public JwtAuthenticationDirector( JWTTokenService jwtTokenServices, IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            this.jwtTokenServices = jwtTokenServices;
            this.unitOfWork = _unitOfWork;
            this.mapper= _mapper; 
            // this.key= key;
        }

        public virtual async Task<Token> Authenticate(string username,string role,string mode,CancellationToken cancellationToken)
        {
            var token = new Token();
            token = jwtTokenServices.TokenGenerate(username, role, mode);
            UpdateRefreshToken(token.RefreshToken, token.CreatedToken, username, mode,"Login", default);
            return  token;
        }
        public virtual async Task<int> UpdateRefreshToken(string refreshToken, string token, string username, string Mode,string Action, CancellationToken cancellationToken)
        {
            //if (mddistrict.Id == "0")
            //{
            //    throw new ArgumentException(nameof(mddistrict.Id));
            //}
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@RefreshToken",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = refreshToken,
                },
                new SqlParameter()
                {
                    ParameterName = "@UserId",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = username,
                },
                new SqlParameter()
                {
                    ParameterName = "@token",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = token,
                },
                new SqlParameter()
                {
                    ParameterName = "@Mode",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = Mode,
                },
                new SqlParameter()
                {
                    ParameterName = "@action",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = Action,
                },
            };
            using (var connection = unitOfWork.HelpDeskDBContext.Database.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "EXEC " + "RefreshToken @UserId,@RefreshToken,@token,@Mode,@action";
                foreach (var parameterDefinition in param)
                {
                    command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                }

                using (var reader = command.ExecuteReader())
                { }
                return 1;
            }
        }
        //public virtual async Task<Token> RefreshToken(string token, string refreshToken, string userName, string mode, CancellationToken cancellationToken)
        //{
        //    var token = new Token();
        //    token = jwtTokenServices.TokenGenerate(username, role, mode);
        //    RefreshToken(token.RefreshToken, token.CreatedToken, username, mode, "Login", default);
        //    return token;
        //}
        public virtual async Task<Abstractions.Models.UserAuthorization> RefreshToken(string token, string refreshToken, string userId,string role, string mode, CancellationToken cancellationToken)
        { 
            var tokentemp = new Token();
            token = token.Substring(7);
                 //AccessToken.Substring(7)
            Data.EF.Models.UserAuthorization UserToken =await this.unitOfWork.UserAuthorizationRepository.FindByAsync(x => x.Token == token && x.RefreshToken == refreshToken && x.UserId == userId && x.Mode == mode, cancellationToken);

            tokentemp = jwtTokenServices.TokenGenerate(userId, role, mode);
            UserToken.Token = tokentemp.CreatedToken;
            UserToken.RefreshToken = tokentemp.RefreshToken;
            await this.unitOfWork.UserAuthorizationRepository.UpdateAsync(UserToken, cancellationToken).ConfigureAwait(false);
           // unitOfWork.HelpDeskDBContext.UserAuthorization.Update(UserToken);// UserAuthorizationRepository.UpdateAsync(, cancellationToken).ConfigureAwait(false);
            await this.unitOfWork.CommitAsync(cancellationToken);
            //unitOfWork.CommitAsync(cancellationToken);
            //var efappServiceRequest = this.mapper.Map<EFModel.AppServiceRequest>(appServiceRequest)
            return this.mapper.Map<Abstractions.Models.UserAuthorization>(UserToken);
        }
    }
}
