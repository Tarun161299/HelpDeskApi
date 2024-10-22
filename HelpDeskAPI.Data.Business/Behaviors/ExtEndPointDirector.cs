//-----------------------------------------------------------------------
// <copyright file="ExtEndPointDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------


namespace HelpDeskAPI.Data.Business.Behaviors
{
    using AutoMapper;
    using HelpDeskAPI.Data.Abstractions.Behaviors;
    using HelpDeskAPI.Data.Abstractions.Models;
    using HelpDeskAPI.Data.Abstractions.Exceptions;
    using HelpDeskAPI.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Azure.Core;
    using Microsoft.Data.SqlClient;
    using AbsModels = HelpDeskAPI.Data.Abstractions.Models;
    using Microsoft.Data.SqlClient;
    using Microsoft.AspNetCore.Http;
    using AutoMapper.Internal;
    using System.Text;
    using HelpDeskAPI.Data.Business.Services;
    using System;
    using System.Linq;
    using System.Security.Cryptography.Xml;
    using Microsoft.Extensions.Configuration;
    using System.Text.Json;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
    using System.Net.Http.Headers;
    using System.Net.Http;

    public class ExtEndPointDirector : IExtEndPointDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UtilityService utilityService;
        private readonly EncryptionDecryptionService decryptionService;
        private readonly SMSService sMSService;
        private readonly IConfiguration configuration;
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtEndPointDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        /// 
        public ExtEndPointDirector(IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService, SMSService _sMSService, EncryptionDecryptionService _encryptionDecryptionService,IConfiguration _configuration)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this._httpContextAccessor = httpContextAccessor;
            this.utilityService = _utilityService;
            this.sMSService = _sMSService;
            this.configuration = _configuration;
        }


        public async Task<int> InsertAsync(string access_token, string expires_in, string refresh_token, string token_type, string token_id,string claim_data, CancellationToken cancellationToken)
        {
            try
            {

                var param = new SqlParameter[]
                {
                new SqlParameter()
                {
                    ParameterName = "@access_token",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = access_token,
                },
                new SqlParameter()
                {
                    ParameterName = "@expires_in",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = expires_in,
                },
                new SqlParameter()
                {
                    ParameterName = "@refresh_token",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = refresh_token,
                },
                new SqlParameter()
                {
                    ParameterName = "@token_type",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = token_type,
                },
                new SqlParameter()
                {
                    ParameterName = "@token_id",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = token_id,
                },
                new SqlParameter()
                {
                    ParameterName = "@claim_data",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = claim_data,
                },
                new SqlParameter()
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output,
                },
                };
                var storedProcedureName = $"{"USP_InsertESSOData"}  @access_token,@expires_in,@refresh_token,@token_type,@token_id,@claim_data,@id output";
                int data = await this.unitOfWork.AppServiceRequestRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
                var requestId = param[6].Value;
                return Convert.ToInt32(requestId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <inheritdoc/>
        public virtual async Task<AbsModels.Esodata> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var eSSODataDetail = await this.unitOfWork.EsodataRepository.FindByAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.Esodata>(eSSODataDetail);
            if(eSSODataDetail != null) 
            {
                await this.unitOfWork.EsodataRepository.DeleteAsync(eSSODataDetail, cancellationToken).ConfigureAwait(false);
                await this.unitOfWork.EsodataRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
            }
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> LogoutAsync(Logout logout, CancellationToken cancellationToken)
        {
            try
            {
                int result = 0;
                string url = configuration.GetSection("LogoutUri").Value.ToString();
                string redirectUrl = url+ "?client_id="+logout.client_id+"&token="+logout.token+"&user_id="+logout.user_id;
                var client = new HttpClient();
                //var body = JsonSerializer.Serialize(logout);

                                
                //var body = JsonSerializer.Serialize(logout);
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(redirectUrl),
                    Method = HttpMethod.Get,                    
                };
                //request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                //request.Content = new StringContent(body, Encoding.UTF8);
                //request.Headers;
                //var response = await client.GetAsync(redirectUrl);
                HttpResponseMessage responseMessage= await client.GetAsync(redirectUrl);
                return result = 1;

                //response.EnsureSuccessStatusCode();
                //var content = await response.Content.ReadAsStringAsync();         
                //var content = new StringContent(body, Encoding.UTF8, "application/json");
                //var response = await client.PostAsync(redirectUrl, content);
                //if (response.IsSuccessStatusCode)
                //{
                //    await response.Content.ReadAsStringAsync();
                    
                //}
                
                return  result;
                
                
                //var res = client.PostAsync(redirectUrl, new StringContent(JsonConvert.SerializeObject(new { logout },Encoding.UTF8, "application/json"));

                /*
                   
                
                 */

                //if (response != null)
                //{
                //    return result = 1;
                //}
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}