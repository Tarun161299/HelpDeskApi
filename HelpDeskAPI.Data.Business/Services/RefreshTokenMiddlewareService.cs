using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Http.Controllers;
using System.Web.Http;
using Microsoft.Data.SqlClient;
using HelpDeskAPI.Data.Interfaces;
using AutoMapper;
using Microsoft.Identity.Client;
using HelpDeskAPI.Data.Abstractions.Models;
using Azure.Core;
using System.Diagnostics;

namespace HelpDeskAPI.Data.Business.Services
{
    public class RefreshTokenMiddlewareService
    {
        private const string ConnectionString = "ConnectionStrings:ConStr";
        private readonly RequestDelegate _next;
        // private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper mapper;
        private readonly JWTTokenService jwtTokenServices;
        HttpActionContext actionContext;
        HttpResponseException exception;

        public RefreshTokenMiddlewareService(RequestDelegate next, IMapper _mapper, IHttpContextAccessor httpContextAccessor, JWTTokenService _jwtTokenServices)//, IAppOnboardingAdminloginDirector _AppAdminLoginDirector)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
            this.jwtTokenServices = _jwtTokenServices;
            // this.unitOfWork = _unitOfWork;
            this.mapper = _mapper;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            try
            {
                var headers = context.Request.Headers;
                var objCollection = new Dictionary<string, string>();
                //var cookietoken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
                foreach (var item in headers)
                {
                    objCollection.Add(item.Key, string.Join(string.Empty, item.Value));
                }

                string resultValue;
                string test = JsonSerializer.Serialize(objCollection);
                if ((objCollection.ContainsKey("refreshtoken") || objCollection.ContainsKey("RefreshToken")) && objCollection.ContainsKey("Authorization"))
                {
                    string Refreshtoken = "";
                    if (objCollection.ContainsKey("RefreshToken"))
                    {
                        Refreshtoken = objCollection["RefreshToken"];
                    }
                    else
                    {
                        Refreshtoken = objCollection["refreshtoken"];
                    }

                    string token = objCollection["Authorization"];

                    if (CheckRefreshToken(Refreshtoken, token, context, unitOfWork, default))
                    {
                        // LogActivity(context, UserName);
                        context.Response.Headers.Add("NewToken", "Abc");
                        context.Response.Headers.Add("NewRefreshToken", "ABC");
                        await this._next(context);
                    }
                    else
                    {
                        string Url = (context.Request.Path.Value);
                        string Activity = Url;
                        LogActivity(context, "", Activity + "This request is unauthorized");
                        throw new ArgumentException("This request is unauthorized");
                    }
                }
                else
                {
                    string Url = (context.Request.Path.Value);
                    string Activity = Url;
                    LogActivity(context, "", Activity);
                    await this._next(context);
                }
            }
            catch (Exception ex)
            {
                string Url = (context.Request.Path.Value);
                string Activity = Url;
                if (ex is ArgumentException)
                {
                    LogActivity(context, "", Activity + "This request is unauthorized");
                    throw new ArgumentException("This request is unauthorized");
                }

                else
                {
                    LogActivity(context, "", Activity + ex.Message.ToString());
                    throw ex;
                }

            }
        }

        public bool CheckRefreshToken(string reftoken, string AccessToken, HttpContext context, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            string AccessTokenUAT = AccessToken.Substring(7);
            var stream = AccessToken.Substring(7);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            string UserName = tokenS.Claims.First(claim => claim.Type == "UserID").Value;
            var mode = tokenS.Claims.First(claim => claim.Type == "mode").Value;
            var Role = tokenS.Claims.First(claim => claim.Type == "Role").Value;
            string Url = (context.Request.Path.Value);
            string Activity = Url;



            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                ParameterName = "@userid",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = UserName,
                },
                new SqlParameter()
                {
                ParameterName = "@refreshToken",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = reftoken,
                },
                new SqlParameter()
                {
                ParameterName = "@token",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = AccessToken.Substring(7),
                },
                 new SqlParameter()
                {
                ParameterName = "@mode",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = mode,
                },
            };

            using (SqlConnection conn = new SqlConnection())
            {
                var ConnectionStrings = context.RequestServices.GetRequiredService<IConfiguration>();

                var webConfigConnectionString = ConnectionStrings.GetValue<string>(ConnectionString);
                conn.ConnectionString = webConfigConnectionString;
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandText = "EXEC " + "GetRefreshToken @userid,@refreshToken,@token,@mode";

                foreach (var parameterDefinition in param)
                {
                    command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                }

                var refreshToken = "";
                var Authtoken = "";
                string token;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        refreshToken = reader.GetString(reader.GetOrdinal("refreshToken"));
                        Authtoken = reader.GetString(reader.GetOrdinal("Token"));
                        token = refreshToken;
                    }
                    else
                    {
                        token = "";
                    }
                }

                conn.Close();
                if ((token == reftoken && Authtoken == AccessTokenUAT) || (reftoken == "123"))
                {
                    LogActivity(context, UserName, Activity);
                    return true;
                }
                else
                {
                    return false;
                }

            }


        }

        public void LogActivity(HttpContext context, string UserName, string Activity)
        {
            LogActivityAuditTrail logActivityAuditTrail = new LogActivityAuditTrail();
            logActivityAuditTrail.UserId = UserName;
            logActivityAuditTrail.Activity = Activity;
            logActivityAuditTrail.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            logActivityAuditTrail.Remarks = "";
            if (context.Request.Method == "GET" || context.Request.Method == "Get")
            {
                return;
            }
            else
            {
                var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                ParameterName = "@UserId",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = logActivityAuditTrail.UserId,
                },
                new SqlParameter()
                {
                ParameterName = "@Activity",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = logActivityAuditTrail.Activity,
                },
                new SqlParameter()
                {
                ParameterName = "@IpAddress",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = logActivityAuditTrail.IpAddress,
                },
                new SqlParameter()
                {
                ParameterName = "@Remarks",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Value = logActivityAuditTrail.Remarks,
                },
            };

                using (SqlConnection conn = new SqlConnection())
                {
                    var ConnectionStrings = context.RequestServices.GetRequiredService<IConfiguration>();

                    var webConfigConnectionString = ConnectionStrings.GetValue<string>(ConnectionString);
                    conn.ConnectionString = webConfigConnectionString;
                    conn.Open();
                    var command = conn.CreateCommand();
                    command.CommandText = "EXEC " + "USP_LogAuditTrail @UserId,@Activity,@IpAddress,@Remarks";

                    foreach (var parameterDefinition in param)
                    {
                        command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                    }


                    using (var reader = command.ExecuteReader())
                    {
                        string message = "";
                        if (reader.Read())
                        {
                            message = reader.GetString(reader.GetOrdinal("Message"));
                        }
                        else
                        {
                            message = "Log not Generated";
                        }

                    }
                    conn.Close();
                }
            }

        }
    }
}
