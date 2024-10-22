
//-----------------------------------------------------------------------
// <copyright file="AppRemarksDirector.cs" company="NIC">
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
    using HelpDeskAPI.Data.Abstractions.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.SqlServer.Server;
    using System.Globalization;
    using System.Reflection;

    /// <inheritdoc />
    public class AppRemarksDirector : IAppRemarksDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRemarksDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppRemarksDirector(IHttpContextAccessor httpContextAccessor,IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppRemarks>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appremarkslist = await this.unitOfWork.AppRemarksRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppRemarks>>(appremarkslist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.AppRemarks> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
            var appremarkslist = await this.unitOfWork.AppRemarksRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppRemarks>(appremarkslist);
            return result;
        }

        public virtual async Task<List<AbsModels.AppRemarks>> GetByModuleIdAsync(AppRemarksData appRemarksData, CancellationToken cancellationToken)
         {
            try
            {
                List<long?> a = new List<long?>();
                var appremarkslist = await this.unitOfWork.AppRemarksRepository.FindAllByAsync(x => x.ModuleId == appRemarksData.ModuleId && x.Module == appRemarksData.Module, cancellationToken).ConfigureAwait(false);
                foreach (var item in appremarkslist)
                {
                    a.Add(item.FileId);
                }
                if (a == null)
                {
                    var appremarkslists = await this.unitOfWork.AppRemarksRepository.FindAllByAsync(x => x.ModuleId == appRemarksData.ModuleId && x.Module == appRemarksData.Module, cancellationToken).ConfigureAwait(false);
                    var remarks = this.mapper.Map<List<Abstractions.Models.AppRemarks>>(appremarkslists);
                    return remarks.ToList();
                }
                else 
                {
                    var documentList = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllByAsync(x => a.Contains(x.DocumentId), cancellationToken).ConfigureAwait(false);
                    var loginDetails = await this.unitOfWork.AppLoginDetailsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

                    var requestlistdata = from appRemark in appremarkslist
                                          join documentlist in documentList on appRemark.FileId equals documentlist.DocumentId into doc
                                          from docs in doc.DefaultIfEmpty()
                                          //join documentlist in documentList on appRemark.FileId equals documentlist.DocumentId
                                          join loginDetail in loginDetails on appRemark.CreatedBy equals loginDetail.UserId
                                          select new HelpDeskAPI.Data.Abstractions.Models.AppRemarks
                                          {
                                              Id = appRemark.Id,
                                              Module = appRemark.Module,
                                              ModuleId = appRemark.ModuleId,
                                              Remarks = appRemark.Remarks,
                                              FileId = appRemark.FileId,
                                              //CreatedDate =DateTime.ParseExact(appRemark.CreatedDate.ToString(), "g", new CultureInfo("fr-FR")),
                                              CreatedDate = appRemark.CreatedDate,
                                              CreatedBy = loginDetail.UserName,
                                              CreatedIp = appRemark.CreatedIp,
                                              IsActive = appRemark.IsActive,
                                              DocContent = docs == null ? null : docs.DocContent,
                                          };
                    var result = this.mapper.Map<List<Abstractions.Models.AppRemarks>>(requestlistdata);
                    return result.OrderByDescending(x=>x.Id).ToList();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.AppRemarks appremarks, CancellationToken cancellationToken)
        {
            if (appremarks == null)
            {
                throw new ArgumentNullException(nameof(appremarks));
            }

            var chkefappremarks = await this.unitOfWork.AppRemarksRepository.FindByAsync(r => r.Id == appremarks.Id, default);
            if (chkefappremarks != null)
            {
                throw new EntityFoundException($"This Records {chkefappremarks} already exists");
            }
            try
            {
                
                if (appremarks.DocType != "")
                {
                    var saveAppDocumentUploadData = new AbsModels.AppDocumentUploadedDetail();
                    saveAppDocumentUploadData.DocType = appremarks.DocType;
                    saveAppDocumentUploadData.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    saveAppDocumentUploadData.SubTime = DateTime.Now;
                    saveAppDocumentUploadData.CreatedBy = appremarks.CreatedBy;
                    saveAppDocumentUploadData.CycleId = appremarks.CycleId;
                    saveAppDocumentUploadData.DocContent = appremarks.DocContent;
                    //saveAppDocumentUploadData.DocSubject = appremarks.Remarks;
                    var efappremarks = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(saveAppDocumentUploadData);

                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(efappremarks, cancellationToken).ConfigureAwait(false);
                    await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                    appremarks.FileId = efappremarks.DocumentId;
                }

                try
                {
                    var saveAppRemarksData = new AbsModels.AppRemarks();
                    saveAppRemarksData.FileId = appremarks.FileId ;
                    saveAppRemarksData.Id = appremarks.Id;
                    saveAppRemarksData.Remarks = appremarks.Remarks;
                    saveAppRemarksData.CreatedIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    saveAppRemarksData.CreatedDate = DateTime.Now;
                    saveAppRemarksData.CreatedBy = appremarks.CreatedBy;
                    saveAppRemarksData.IsActive = appremarks.IsActive;
                    saveAppRemarksData.Module = appremarks.Module;
                    saveAppRemarksData.ModuleId = appremarks.ModuleId;
                    var efappremark = this.mapper.Map<Data.EF.Models.AppRemarks>(saveAppRemarksData);

                    await this.unitOfWork.AppRemarksRepository.InsertAsync(efappremark, cancellationToken).ConfigureAwait(false);
                    return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>

        public virtual async Task<int> UpdateAsync(AbsModels.AppRemarks appremarks, CancellationToken cancellationToken)
        
		{
            if (appremarks.Id == 0)
            {
                throw new ArgumentException(nameof(appremarks.Id));
            }
			
			Data.EF.Models.AppRemarks entityUpd = await unitOfWork.AppRemarksRepository.FindByAsync(e => e.Id == appremarks.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= appremarks.Id;
					entityUpd.Module= appremarks.Module;
					entityUpd.ModuleId= appremarks.ModuleId;
					entityUpd.Remarks= appremarks.Remarks;
					entityUpd.CreatedDate= appremarks.CreatedDate;
					entityUpd.CreatedBy= appremarks.CreatedBy;
					entityUpd.CreatedIp= appremarks.CreatedIp;
					entityUpd.IsActive= appremarks.IsActive;
					
				await unitOfWork.AppRemarksRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int Id, CancellationToken cancellationToken)
        {
            if (Id == 0)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.AppRemarksRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.AppRemarksRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppRemarksRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	