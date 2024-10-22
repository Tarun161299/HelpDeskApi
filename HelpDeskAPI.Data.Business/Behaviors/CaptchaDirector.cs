//-----------------------------------------------------------------------
// <copyright file="CaptchaDirector.cs" company="NIC">
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

	public class CaptchaDirector : ICaptchaDirector
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly IMapper mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UtilityService utilityService;
		private readonly EncryptionDecryptionService decryptionService;
		private readonly SMSService sMSService;
		/// <summary>
		/// Initializes a new instance of the <see cref="CaptchaDirector"/> class.
		/// </summary>
		/// <param name="mapper">Automapper.</param>
		/// <param name="unitOfWork">Unit of Work.</param>
		/// 
		public CaptchaDirector(IHttpContextAccessor httpContextAccessor, IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService, SMSService _sMSService, EncryptionDecryptionService _encryptionDecryptionService)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this._httpContextAccessor = httpContextAccessor;
			this.utilityService = _utilityService;
			this.sMSService = _sMSService;
		}


		public virtual async Task<AbsModels.AppCaptcha> InsertAsync(string key, string base64String, string hashvalue, CancellationToken cancellationToken)
		{
			var cc = new AbsModels.AppCaptcha();
			cc.Md5Hash = hashvalue;
			cc.Ip = "::1";
			cc.CaptchaKey = key;
			cc.CaptchBaseString = base64String;

			try
			{

				var param = new SqlParameter[]
				{
						 new SqlParameter()
						 {
							 ParameterName = "@key",
							 SqlDbType = System.Data.SqlDbType.VarChar,
							 Value = key,
						 },
						 new SqlParameter()
						 {
							 ParameterName = "@base64String",
							 SqlDbType = System.Data.SqlDbType.NVarChar,
							 Value = base64String,
						 },
						 new SqlParameter()
						 {
							 ParameterName = "@hashvalue",
							 SqlDbType = System.Data.SqlDbType.VarChar,
							 Value = hashvalue,
						 },
				 new SqlParameter()
				{
					ParameterName = "@ip",
					SqlDbType = System.Data.SqlDbType.VarChar,
					Value = "::1",
				},
	          };
				var storedProcedureName = $"{"Usp_Insertcaptcha"}  @key,@base64String,@hashvalue,@ip";
				var data = await this.unitOfWork.AppCaptchaRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
				var ef = this.mapper.Map<Data.Abstractions.Models.AppCaptcha>(cc);
				if (data > 0)
				{
					return ef;
				}
				else
				{
					return null;

				}

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}


		/// <inheritdoc/>
		public virtual async Task<AbsModels.Esodata> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var eSSODataDetail = await this.unitOfWork.EsodataRepository.FindByAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
			var result = this.mapper.Map<Abstractions.Models.Esodata>(eSSODataDetail);
			if (eSSODataDetail != null)
			{
				await this.unitOfWork.EsodataRepository.DeleteAsync(eSSODataDetail, cancellationToken).ConfigureAwait(false);
				await this.unitOfWork.EsodataRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
			}
			return result;
		}

		public virtual async Task<int> checkCaptcha(Check_captcha captcha, CancellationToken cancellationToken)
		{

			try
			{

				var param = new SqlParameter[]
				{
						 new SqlParameter()
						 {
							 ParameterName = "@key",
							 SqlDbType = System.Data.SqlDbType.VarChar,
							 Value =captcha.key,
						 },

                          new SqlParameter()
                         {
                             ParameterName = "@hash",
                             SqlDbType = System.Data.SqlDbType.VarChar,
                             Value =captcha.hash,
                         },
                         new SqlParameter()
						 {
							 ParameterName = "@check",
							 SqlDbType = System.Data.SqlDbType.Bit,
							 Direction = System.Data.ParameterDirection.Output,
						 },

	   };
				var storedProcedureName = $"{"Check_Captcha"}  @key,@hash,@check output";
				var data = await this.unitOfWork.AppCaptchaRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
				bool s = (bool)param[2].Value;
				if (s == true)
				{
					return 1;
				}
				else
				{
					return 0;

				}

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

	}

}