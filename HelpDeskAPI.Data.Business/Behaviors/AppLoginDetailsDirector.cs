
//-----------------------------------------------------------------------
// <copyright file="AppLoginDetailsDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace HelpDeskAPI.Data.Business.Behaviors
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using HelpDeskAPI.Data.Abstractions.Behaviors;
    using HelpDeskAPI.Data.Abstractions.Exceptions;
    using AbsModels = HelpDeskAPI.Data.Abstractions.Models;
    using HelpDeskAPI.Data.Interfaces;
    using Microsoft.AspNetCore.Http;
    using System.Text;
    using HelpDeskAPI.Data.Business.Services;

    /// <inheritdoc />
    public class AppLoginDetailsDirector : IAppLoginDetailsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UtilityService utilityService;
        private readonly EncryptionDecryptionService decryptionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppLoginDetailsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppLoginDetailsDirector(UtilityService _utilityService, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, EncryptionDecryptionService _encryptionDecryptionService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.utilityService = _utilityService;
            this.decryptionService = _encryptionDecryptionService;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppLoginDetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            var applogindetailslist = await this.unitOfWork.AppLoginDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppLoginDetails>>(applogindetailslist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.AppLoginDetails> GetByIdAsync(string UserId, CancellationToken cancellationToken)
        {
            string decryptedUserId = decryptionService.Decryption(UserId);
            //string decryptedUserId = Encoding.UTF8.GetString(Convert.FromBase64String(UserId));
            var applogindetailslist = await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(x => x.UserId == decryptedUserId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppLoginDetails>(applogindetailslist);
            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.AppLoginDetails> GetByEisIdAsync(string eisId, CancellationToken cancellationToken)
        {
            //string decryptedUserId = decryptionService.Decryption(eisId);
            //string decryptedUserId = Encoding.UTF8.GetString(Convert.FromBase64String(UserId));
            var applogindetailslist = await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(x => x.EisUserId == eisId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppLoginDetails>(applogindetailslist);
            return result;
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.AppLoginDetails applogindetails, CancellationToken cancellationToken)
        {
            if (applogindetails == null)
            {
                throw new ArgumentNullException(nameof(applogindetails));
            }

            var chkefapplogindetails = await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(r => r.UserId == applogindetails.UserId, default);
            if (chkefapplogindetails != null)
            {
                throw new EntityFoundException($"This Records {chkefapplogindetails} already exists");
            }

            var efapplogindetails = this.mapper.Map<Data.EF.Models.AppLoginDetails>(applogindetails);

            await this.unitOfWork.AppLoginDetailsRepository.InsertAsync(efapplogindetails, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.AppLoginDetails applogindetails, CancellationToken cancellationToken)
        
		{
            if (applogindetails.UserId == "0")
            {
                throw new ArgumentException(nameof(applogindetails.UserId));
            }
			
			Data.EF.Models.AppLoginDetails entityUpd = await unitOfWork.AppLoginDetailsRepository.FindByAsync(e => e.UserId == applogindetails.UserId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.UserId= applogindetails.UserId;
					entityUpd.UserName= applogindetails.UserName;
					entityUpd.IsActive= applogindetails.IsActive;
					entityUpd.LastLoginTime= applogindetails.LastLoginTime;
					entityUpd.LastLoginIp= applogindetails.LastLoginIP;
					entityUpd.Mobile= applogindetails.Mobile;
					entityUpd.Email= applogindetails.Email;
					
				await unitOfWork.AppLoginDetailsRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string UserId, CancellationToken cancellationToken)
        {
            if (UserId == "0")
            {
                throw new ArgumentNullException(nameof(UserId));
            }

            var entity = await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(x => x.UserId == UserId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an UserId {UserId} was not found.");
            }

            await this.unitOfWork.AppLoginDetailsRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppLoginDetailsRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}

        public virtual async Task<bool> CheckUserIdAvailibity(string userID, CancellationToken cancellationToken)
        {

            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@UserID",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = userID,
                },
                new SqlParameter()
                {
                    ParameterName = "@IsError",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output,
                },
            };

            var storedProcedureName = $"{"USP_CheckUserIdAvailibilty"}  @UserID,@IsError output";
            int result = await this.unitOfWork.AppLoginDetailsRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
            bool s = (bool)param[1].Value;
            return s;
        }

        public virtual async Task<int> SaveSignUpDetailsAsync(Abstractions.Models.SignUp signUpData, CancellationToken cancellationToken)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@UserID",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = signUpData.UserID,
                },
                
                new SqlParameter()
                {
                    ParameterName = "@UserName",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = signUpData.UserName,
                },
                new SqlParameter()
                {
                    ParameterName = "@Mobile",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = signUpData.Mobile,
                },
                new SqlParameter()
                {
                    ParameterName = "@Email",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = signUpData.Email,
                },
                new SqlParameter()
                {
                    ParameterName = "@LastLoginIP",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                },

            };

            var storedProcedureName = $"{"USP_InsertSignUpDetail"}  @UserID,@UserName,@Mobile,@Email,@LastLoginIP";
            return await this.unitOfWork.AppLoginDetailsRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public virtual async Task<string> GetCaptcha(CancellationToken cancellationToken)
        {
            string captcha;
            var response = this.utilityService.RandomString(6, true);
            return response;
        }
    }

	}
	