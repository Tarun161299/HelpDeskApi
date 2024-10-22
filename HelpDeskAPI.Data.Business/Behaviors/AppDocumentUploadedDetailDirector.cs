
//-----------------------------------------------------------------------
// <copyright file="AppDocumentUploadedDetailDirector.cs" company="NIC">
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
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using System.Text.Json;
    using HelpDeskAPI.Data.Business.Services;

    /// <inheritdoc />
    public class AppDocumentUploadedDetailDirector : IAppDocumentUploadedDetailDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EncryptionDecryptionService decryptionService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDocumentUploadedDetailDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppDocumentUploadedDetailDirector(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, EncryptionDecryptionService _encryptionDecryptionService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            this.decryptionService = _encryptionDecryptionService;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppDocumentUploadedDetail>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appdocumentuploadeddetaillist = await this.unitOfWork.AppDocumentUploadedDetailRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppDocumentUploadedDetail>>(appdocumentuploadeddetaillist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.AppDocumentUploadedDetail> GetByIdAsync(int fileId, CancellationToken cancellationToken)
        {
            var appdocumentuploadeddetaillist = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(x => x.DocumentId == fileId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppDocumentUploadedDetail>(appdocumentuploadeddetaillist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.UpdateDocuments appdocumentuploadeddetail, CancellationToken cancellationToken)
        {
            try
            {
                //if (appdocumentuploadeddetail == null)
                //{
                //    throw new ArgumentNullException(nameof(appdocumentuploadeddetail));
                //}

                //var chkefappdocumentuploadeddetail = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(r => r.DocumentId == appdocumentuploadeddetail.DocumentId, default);
                //if (chkefappdocumentuploadeddetail != null)
                //{
                //    throw new EntityFoundException($"This Records {chkefappdocumentuploadeddetail} already exists");
                //}

                //var efappdocumentuploadeddetail = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(appdocumentuploadeddetail);

                //await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(efappdocumentuploadeddetail, cancellationToken).ConfigureAwait(false);
                //return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                var jsonString = JsonSerializer.Serialize(appdocumentuploadeddetail);
                var param = new SqlParameter[]
                {
                new SqlParameter()
                {
                    ParameterName = "@InputJson",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = jsonString,
                },
                 new SqlParameter()
                {
                    ParameterName = "@mode",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appdocumentuploadeddetail.mode,
                },
                  new SqlParameter()
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = appdocumentuploadeddetail.id,
                },
                new SqlParameter()
                {
                    ParameterName = "@IsError",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output,
                },
                 new SqlParameter()
                {
                    ParameterName = "@FileNO",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output,
                },
                    //new SqlParameter()
                    //{
                    //    ParameterName = "@Message",   
                    //    SqlDbType = System.Data.SqlDbType.VarChar,
                    //    Direction = System.Data.ParameterDirection.Output,
                    //},
                };
                var storedProcedureName = $"{"Sp_UpdateAppDocumentUploadedDetail"}  @InputJson,@mode,@id,@IsError output,@FileNO output";
                int result = await this.unitOfWork.AppDocumentUploadedDetailRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
                bool s = (bool)param[3].Value;
                int FileId = (int)param[4].Value;
                if (s == true)
                {
                    return FileId;
                }
                else return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.AppDocumentUploadedDetail appdocumentuploadeddetail, CancellationToken cancellationToken)        
		{
            if (appdocumentuploadeddetail.DocumentId == 0)
            {
                throw new ArgumentException(nameof(appdocumentuploadeddetail.DocumentId));
            }

            string decryptedCreatedBy = decryptionService.Decryption(appdocumentuploadeddetail.CreatedBy);
            //string decryptedCreatedBy = Encoding.UTF8.GetString(Convert.FromBase64String(appdocumentuploadeddetail.CreatedBy));
            Data.EF.Models.AppDocumentUploadedDetail entityUpd = await unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(e => e.DocumentId == appdocumentuploadeddetail.DocumentId, cancellationToken);
			if (entityUpd != null)
            {
			        entityUpd.DocumentId= appdocumentuploadeddetail.DocumentId;
					entityUpd.Activityid= appdocumentuploadeddetail.Activityid;
					entityUpd.CycleId= appdocumentuploadeddetail.CycleId;
					entityUpd.DocType= appdocumentuploadeddetail.DocType;
					entityUpd.DocId= appdocumentuploadeddetail.DocId;
					entityUpd.DocSubject= appdocumentuploadeddetail.DocSubject;
					entityUpd.DocContent= appdocumentuploadeddetail.DocContent;
					entityUpd.ObjectId= appdocumentuploadeddetail.ObjectId;
					entityUpd.ObjectUrl= appdocumentuploadeddetail.ObjectUrl;
					entityUpd.DocNatureId= appdocumentuploadeddetail.DocNatureId;
					entityUpd.IpAddress= _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    entityUpd.SubTime= DateTime.Now;
					entityUpd.CreatedBy= decryptedCreatedBy;
					
				await unitOfWork.AppDocumentUploadedDetailRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int DocumentId, CancellationToken cancellationToken)
        {
            if (DocumentId == 0)
            {
                throw new ArgumentNullException(nameof(DocumentId));
            }

            var entity = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(x => x.DocumentId == DocumentId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an DocumentId {DocumentId} was not found.");
            }

            await this.unitOfWork.AppDocumentUploadedDetailRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppDocumentUploadedDetailRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	