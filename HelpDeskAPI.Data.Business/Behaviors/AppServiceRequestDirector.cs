//-----------------------------------------------------------------------
// <copyright file="AppServiceRequestDirector.cs" company="NIC">
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
    using EFModel = HelpDeskAPI.Data.EF.Models;
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
    using static System.Collections.Specialized.BitVector32;
    using Microsoft.Extensions.Logging;
    using System.Security.Cryptography;

   
    using System.Net;
    using System.Reflection.Metadata;
    using System.Xml.Linq;
    using System.Threading;

    public class AppServiceRequestDirector : IAppServiceRequestDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UtilityService utilityService;
        private readonly EncryptionDecryptionService decryptionService;
        private readonly SMSService sMSService;
        private readonly ILogger<AppServiceRequestDirector> logger;
//        public AppServiceRequestDirector(IHttpContextAccessor httpContextAccessor, EncryptionDecryptionService _decryptionService, IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService, SMSService _sMSService, EncryptionDecryptionService _encryptionDecryptionService)
//=========
//<<<<<<<<< Temporary merge branch 1
//        public AppServiceRequestDirector(IHttpContextAccessor httpContextAccessor, EncryptionDecryptionService _decryptionService, IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService, SMSService _sMSService, EncryptionDecryptionService _encryptionDecryptionService)
//=========
//        /// <param name="mapper">Automapper.</param>
//        /// 
//<<<<<<<<< Temporary merge branch 1
//        public AppServiceRequestDirector(IHttpContextAccessor httpContextAccessor, EncryptionDecryptionService _decryptionService, IMapper mapper, IUnitOfWork unitOfWork, UtilityService _utilityService, SMSService _sMSService, EncryptionDecryptionService _encryptionDecryptionService)
//=========
        public AppServiceRequestDirector(IHttpContextAccessor _httpContextAccessor,IMapper _mapper, IUnitOfWork _unitOfWork, UtilityService _utilityService, SMSService _sMSService, EncryptionDecryptionService _encryptionDecryptionService, ILogger<AppServiceRequestDirector> _logger)
        {
            this.mapper = _mapper;
            this._httpContextAccessor = _httpContextAccessor;
            this.utilityService = _utilityService;
            this.sMSService = _sMSService;
            this.decryptionService = _encryptionDecryptionService;
            this.logger = _logger;
            this.unitOfWork = _unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AppServiceRequestsList>> GetAllAsync(CancellationToken cancellationToken)
        {
            var projectid=await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false); ;
            var appServiceRequestRepository = await this.unitOfWork.AppServiceRequestRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var requestlistdata = from servicerequests in appServiceRequestRepository
                                  join projects in projectid on servicerequests.BoardId equals projects.ProjectId
                                  select new HelpDeskAPI.Data.Abstractions.Models.AppServiceRequestsList
                                  {
                                      ServiceRequestId = servicerequests.ServiceRequestId,
                                      ServiceRequestNo = servicerequests.ServiceRequestNo,
                                      BoardId = (servicerequests.BoardId).ToString(),
                                      BoardName = projects.ProjectName,
                                      RequestCategoryIds = servicerequests.RequestCategoryIds,
                                      Subject = servicerequests.Subject,
                                      Description = servicerequests.Description,
                                      Status = (servicerequests.Status=="P")?"Pending": (servicerequests.Status == "A")?"Accepted": (servicerequests.Status == "D")?"Discard":(servicerequests.Status == "T")?"Return":(servicerequests.Status =="R")?"Reject": "Not Status",
                                      Priority = servicerequests.Priority,
                                      ResolutionDate = servicerequests.ResolutionDate,
                                      UserId = servicerequests.UserId,
                                      CreatedDate = servicerequests.CreatedDate,
                                      CreatedBy = servicerequests.CreatedBy,
                                      CreatedIp = servicerequests.CreatedIp,
                                      ModifiedDate = servicerequests.ModifiedDate,
                                      ModifiedBy = servicerequests.ModifiedBy,
                                      ModifiedIp = servicerequests.ModifiedIp
                                  };

            return requestlistdata.ToList();
        }

        public async Task<int> InsertAsync(AppServiceRequest appServiceRequest, CancellationToken cancellationToken)
            {
            try
            {
                string userId = decryptionService.Decryption(appServiceRequest.UserId);
                string createdBy = decryptionService.Decryption(appServiceRequest.CreatedBy);

                //string userId = Encoding.UTF8.GetString(Convert.FromBase64String(appServiceRequest.UserId));
                //string createdBy = Encoding.UTF8.GetString(Convert.FromBase64String(appServiceRequest.CreatedBy));

                if (appServiceRequest == null)
                {
                    throw new ArgumentNullException(nameof(appServiceRequest));
                }

                //var chkefappServiceRequest = await this.unitOfWork.AppServiceRequestRepository.FindByAsync(r => r.BoardId == appServiceRequest.BoardId, default);
                //if (chkefappServiceRequest != null)
                //{
                //    throw new EntityFoundException($"This Records {chkefappServiceRequest} already exists");
                //}

                //var efappServiceRequest = this.mapper.Map<EFModel.AppServiceRequest>(appServiceRequest);

                //await this.unitOfWork.AppServiceRequestRepository.InsertAsync(efappServiceRequest, cancellationToken).ConfigureAwait(false);
                //return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                var param = new SqlParameter[]
                {
                new SqlParameter()
                {
                    ParameterName = "@BoardId",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = appServiceRequest.BoardId,
                },
                new SqlParameter()
                {
                    ParameterName = "@RequestCategoryIds",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appServiceRequest.RequestCategoryIds,
                },
                new SqlParameter()
                {
                    ParameterName = "@Subject",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appServiceRequest.Subject,
                },
                new SqlParameter()
                {
                    ParameterName = "@Description",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appServiceRequest.Description,
                },
                new SqlParameter()
                {
                    ParameterName = "@Status",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appServiceRequest.Status,
                },
                new SqlParameter()
                {
                    ParameterName = "@Priority",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = appServiceRequest.Priority,
                },
                new SqlParameter()
                {
                    ParameterName = "@ResolutionDate",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Value = appServiceRequest.ResolutionDate,
                },
                new SqlParameter()
                {
                    ParameterName = "@UserId",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = userId,
                },

                new SqlParameter()
                {
                    ParameterName = "@CreatedBy",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = createdBy,
                },
                  new SqlParameter()
                {
                    ParameterName = "@IpAddress",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                },
                //new SqlParameter()
                //{
                //    ParameterName = "@fileExtension",
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Value = (appServiceRequest.fileExtension==null)? DBNull.Value:appServiceRequest.fileExtension,
                //},
                //new SqlParameter()
                //{
                //    ParameterName = "@content",
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Value = (appServiceRequest.content==null)? DBNull.Value:appServiceRequest.content,
                //},
                 new SqlParameter()
                {
                    ParameterName = "@fileExtension",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = (appServiceRequest.fileExtension==null)?"":appServiceRequest.fileExtension,
                },
                new SqlParameter()
                {
                    ParameterName = "@content",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = (appServiceRequest.content==null)? "":appServiceRequest.content,
                },



            };
                var storedProcedureName = $"{"USP_InsertAppServiceRequest"}  @BoardId,@RequestCategoryIds,@Subject,@Description,@Status,@Priority,@ResolutionDate,@UserId,@CreatedBy,@IpAddress,@fileExtension,@content";
                int data = await this.unitOfWork.AppServiceRequestRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
                var MdUSerRole=await this.unitOfWork.MdUserBoardRoleMappingRepository.FindByAsync(r=>r.BoardId== appServiceRequest.BoardId && r.RoleId==31, cancellationToken).ConfigureAwait(false);
                var AppLogin= await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(r=>r.UserId== MdUSerRole.UserId, cancellationToken).ConfigureAwait(false);
                MailRequest mailtoboard = new MailRequest();
                var emaiTemplateUser = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0011", cancellationToken).ConfigureAwait(false);
                string Body = emaiTemplateUser.MessageTemplate;
                Body = Body.Replace("#Status#", "Assigned");
                mailtoboard.Body = Body;
                mailtoboard.Subject = "Service Request Created";
                mailtoboard.ToEmail = (AppLogin.Email).Trim();
                 mailtoboard.Body = Body;
                var mail = utilityService.SendEmailAsync(mailtoboard);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            

        /// <inheritdoc/>
        public virtual async Task<List< AbsModels.AppServiceRequestsList>> GetByIdAsync(GetServiceByUserAndStatus Userdetails, CancellationToken cancellationToken)
        {
            try
            {

               // var mdsectionlist = await this.unitOfWork.MdSectionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                
                string decryptedUserId = decryptionService.Decryption(Userdetails.UserId);
                var zmstprojectslist = await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var mdUserBoardRoleMappingList = await this.unitOfWork.MdUserBoardRoleMappingRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var projectid = await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var mdSectionList = await this.unitOfWork.MdSectionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var mdStatusList = await this.unitOfWork.MdStatusRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var absmdsectionlist = this.mapper.Map<List<MdSection>>(mdSectionList);
                var appServiceRequestRepository = await this.unitOfWork.AppServiceRequestRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var boardId = from mdUserBoardRoleMapping in mdUserBoardRoleMappingList
                              join zmstprojects in zmstprojectslist on mdUserBoardRoleMapping.BoardId equals zmstprojects.ProjectId
                              where (mdUserBoardRoleMapping.UserId).Trim().ToLower() == decryptedUserId.ToLower()
                              select new AbsModels.ZmstProjects
                              {
                                  ProjectId = zmstprojects.ProjectId,
                                  ProjectName = zmstprojects.ProjectName,
                              };
                var requestlistdata = from servicerequests in appServiceRequestRepository
                                      join projects in projectid on servicerequests.BoardId equals projects.ProjectId
                                      join status in mdStatusList on servicerequests.Status equals status.Id
                                      select new HelpDeskAPI.Data.Abstractions.Models.AppServiceRequestsList
                                      {
                                          ServiceRequestId= servicerequests.ServiceRequestId,
                                          ServiceRequestNo = servicerequests.ServiceRequestNo,
                                          BoardId = (servicerequests.BoardId).ToString(),
                                          BoardName = projects.ProjectName,
                                          RequestCategoryIds = ConvertIdToCategoryName(servicerequests.RequestCategoryIds, absmdsectionlist),// servicerequests.RequestCategoryIds,
                                          Subject = servicerequests.Subject,
                                          Description = servicerequests.Description,
                                          Status = status.Description,
                                          Priority = servicerequests.Priority,
                                          ResolutionDate = servicerequests.ResolutionDate,
                                          UserId = servicerequests.UserId,
                                          CreatedDate = servicerequests.CreatedDate,
                                          CreatedBy = servicerequests.CreatedBy,
                                          CreatedIp = servicerequests.CreatedIp,
                                          ModifiedDate = servicerequests.ModifiedDate,
                                          ModifiedBy = servicerequests.ModifiedBy,
                                          ModifiedIp = servicerequests.ModifiedIp,
                                          StatusId = status.Id
                                      };

                //var result = Nullable;
                var finalData = from reqdata in requestlistdata
                                join Board in boardId on reqdata.BoardId equals Board.ProjectId.ToString()
                                // join section in mdSectionList on reqdata.RequestCategoryIds equals section.SectionId.ToString()
                                select new HelpDeskAPI.Data.Abstractions.Models.AppServiceRequestsList
                                {
                                    ServiceRequestId = reqdata.ServiceRequestId,
                                    ServiceRequestNo = reqdata.ServiceRequestNo,
                                    BoardId = (reqdata.BoardId).ToString(),
                                    BoardName = reqdata.BoardName,
                                    RequestCategoryIds = ConvertIdToCategoryName(reqdata.RequestCategoryIds, absmdsectionlist),
                                    //reqdata.RequestCategoryIds,
                                    Subject = reqdata.Subject,
                                    Description = reqdata.Description,
                                    //Status = (reqdata.Status == "P") ? "Pending" : (reqdata.Status == "A") ? "Accepted" : (reqdata.Status == "D") ? "Discard" : (reqdata.Status == "T") ? "Return" : (reqdata.Status == "R") ? "Reject" : "Not Status",
                                    Status = reqdata.Status,
                                    Priority = reqdata.Priority,
                                    ResolutionDate = reqdata.ResolutionDate,
                                    UserId = reqdata.UserId,
                                    CreatedDate = reqdata.CreatedDate,
                                    CreatedBy = reqdata.CreatedBy,
                                    CreatedIp = reqdata.CreatedIp,
                                    ModifiedDate = reqdata.ModifiedDate,
                                    ModifiedBy = reqdata.ModifiedBy,
                                    ModifiedIp = reqdata.ModifiedIp,
                                    Section = reqdata.RequestCategoryIds,
                                    StatusId = reqdata.StatusId
                                };
                //List<AppServiceRequestsList> services = new List<AppServiceRequestsList>();
                //foreach (var item in boardId)
                //{
                //    var result = services = this.mapper.Map<List<AppServiceRequestsList>>(requestlistdata.Where(s => s.BoardId.Contains((item.ProjectId).toString())));
                //    services.Add(result.ToList());
                //}
                //var finaldata = requestlistdata.contains(boardid.)
                //  services =    this.mapper.Map<List<AppServiceRequest>>(requestlistdata.Where(s => s.BoardId.Contains());
                if (Userdetails.Status == "SA" || Userdetails.Status == "ST" || Userdetails.Status == "SC" || Userdetails.Status == "SR")
                {
                    var serviceAccepted = from serviceAccept in finalData
                                          where serviceAccept.StatusId == Userdetails.Status
                                          select new HelpDeskAPI.Data.Abstractions.Models.AppServiceRequestsList
                                          {
                                              ServiceRequestId = serviceAccept.ServiceRequestId,
                                              ServiceRequestNo = serviceAccept.ServiceRequestNo,
                                              BoardId = (serviceAccept.BoardId).ToString(),
                                              BoardName = serviceAccept.BoardName,
                                              RequestCategoryIds = ConvertIdToCategoryName(serviceAccept.RequestCategoryIds, absmdsectionlist),
                                              //serviceAccept.RequestCategoryIds,
                                              Subject = serviceAccept.Subject,
                                              Description = serviceAccept.Description,
                                              //Status = (reqdata.Status == "P") ? "Pending" : (reqdata.Status == "A") ? "Accepted" : (reqdata.Status == "D") ? "Discard" : (reqdata.Status == "T") ? "Return" : (reqdata.Status == "R") ? "Reject" : "Not Status",
                                              Status = serviceAccept.Status,
                                              Priority = serviceAccept.Priority,
                                              ResolutionDate = serviceAccept.ResolutionDate,
                                              UserId = serviceAccept.UserId,
                                              CreatedDate = serviceAccept.CreatedDate,
                                              CreatedBy = serviceAccept.CreatedBy,
                                              CreatedIp = serviceAccept.CreatedIp,
                                              ModifiedDate = serviceAccept.ModifiedDate,
                                              ModifiedBy = serviceAccept.ModifiedBy,
                                              ModifiedIp = serviceAccept.ModifiedIp,
                                              Section = serviceAccept.Section,
                                              StatusId = serviceAccept.StatusId
                                          };
                    return  serviceAccepted.DistinctBy(x => x.ServiceRequestNo).OrderByDescending(x => x.ServiceRequestId).ToList();
                }


                return finalData.DistinctBy(x => x.ServiceRequestNo).OrderByDescending(x => x.ServiceRequestId).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }

        public virtual async Task<List<AbsModels.AppServiceRequestsList>> GetByRequestIdAsync(string serviceRequestId, CancellationToken cancellationToken)
        {
            try
            {
                long? Fileid = 0;
                FileId fileid;
                string decryptedserviceRequestId = decryptionService.Decryption(serviceRequestId);
                //string decryptedserviceRequestId = Encoding.UTF8.GetString(Convert.FromBase64String(serviceRequestId));
                var appServiceRequestRepository = await this.unitOfWork.AppServiceRequestRepository.FindAllByAsync(x => x.ServiceRequestNo == decryptedserviceRequestId, cancellationToken).ConfigureAwait(false);
                var fileId = from serviceId in appServiceRequestRepository
                             where serviceId.ServiceRequestNo == decryptedserviceRequestId
                             select new FileId
                             {
                                 fileId = serviceId.FileId
                             };

                foreach (FileId item in fileId)
                {
                    Fileid = item.fileId;
                }
                if (Fileid == null)
                {
                    Fileid = 0;
                }
                int a = Convert.ToInt32(Fileid);
                var mdSectionList = await this.unitOfWork.MdSectionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var absmdsectionlist = this.mapper.Map<List<MdSection>>(mdSectionList);
                var zmstprojectslist = await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var priority = await this.unitOfWork.MdPriorityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var appDocumentUploadedDetail = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllByAsync(x => x.DocumentId == Convert.ToInt32(Fileid), cancellationToken).ConfigureAwait(false);
                var mdStatus = await this.unitOfWork.MdStatusRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var result = from servicerequests in appServiceRequestRepository
                                 //join section in mdSectionList on servicerequests.RequestCategoryIds equals section.SectionId.ToString()
                             join zmstprojects in zmstprojectslist on servicerequests.BoardId equals zmstprojects.ProjectId
                             join appDocumentUpload in appDocumentUploadedDetail on ((servicerequests.FileId == null) ? 0 : servicerequests.FileId) equals appDocumentUpload.DocumentId
                              into temp
                             from appDocumentUpload in temp.DefaultIfEmpty()
                             join status in mdStatus on servicerequests.Status equals status.Id
                             join prior in priority on servicerequests.Priority equals (prior.PriorityId).ToString()
                             where servicerequests.ServiceRequestNo == decryptedserviceRequestId
                             select new HelpDeskAPI.Data.Abstractions.Models.AppServiceRequestsList
                             {
                                 ServiceRequestId = servicerequests.ServiceRequestId,
                                 ServiceRequestNo = servicerequests.ServiceRequestNo,
                                 BoardId = (servicerequests.BoardId).ToString(),
                                 BoardName = zmstprojects.ProjectName,
                                 RequestCategoryIds = ConvertIdToCategoryName(servicerequests.RequestCategoryIds, absmdsectionlist),
                                 Subject = servicerequests.Subject,
                                 Description = servicerequests.Description,
                                 Status = status.Description,
                                 Priority = prior.Description,
                                 ResolutionDate = servicerequests.ResolutionDate,
                                 UserId = servicerequests.UserId,
                                 CreatedDate = servicerequests.CreatedDate,
                                 CreatedBy = servicerequests.CreatedBy,
                                 CreatedIp = servicerequests.CreatedIp,
                                 ModifiedDate = servicerequests.ModifiedDate,
                                 ModifiedBy = servicerequests.ModifiedBy,
                                 ModifiedIp = servicerequests.ModifiedIp,
                                 Section = servicerequests.RequestCategoryIds,
                                 FileId = servicerequests.FileId,
                                 DocContent = (appDocumentUpload == null) ? "" : appDocumentUpload.DocContent

                             };
                return result.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(int ServiceRequestId, CancellationToken cancellationToken)
        {
            if (ServiceRequestId == 0)
            {
                throw new ArgumentNullException(nameof(ServiceRequestId));
            }

            var entity = await this.unitOfWork.AppServiceRequestRepository.FindByAsync(x => x.ServiceRequestId == ServiceRequestId, cancellationToken).ConfigureAwait(false);
            var ticket=await this.unitOfWork.AppTicketRepository.FindAllByAsync(x=>x.ServiceRequestId == ServiceRequestId,cancellationToken).ConfigureAwait(false);
            if (ticket.Count()!=0)
            {
                return 0;
                
            }

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ServiceRequestId {ServiceRequestId} was not found.");
            }

            await this.unitOfWork.AppServiceRequestRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppServiceRequestRepository.CommitAsync(cancellationToken).ConfigureAwait(false);

        }
        public async Task<int> UpdateAsync(AppServiceRequest appServiceRequest, CancellationToken cancellationToken)
        {
            if (appServiceRequest == null)
            {
                throw new ArgumentNullException(nameof(appServiceRequest));
            }

            string decryptedModifiedby = decryptionService.Decryption(appServiceRequest.ModifiedBy);
            string decryptedUserUd = decryptionService.Decryption(appServiceRequest.UserId);
            if (appServiceRequest.Status== "SC")
            {
                var tickets= await this.unitOfWork.AppTicketRepository.FindAllByAsync(r => r.ServiceRequestId == appServiceRequest.ServiceRequestId &&( r.TaskStatus=="PI" || r.TaskStatus=="PC" || r.TaskStatus == "PR" ||  r.TaskStatus == "PN"), default);
                if (tickets.Count() > 0)
                {
                    return 999;
                }
            }
            var chkefappServiceRequest = await this.unitOfWork.AppServiceRequestRepository.FindByAsync(r => r.ServiceRequestNo == appServiceRequest.ServiceRequestNo, default);
          
            if (chkefappServiceRequest != null)
            {
                chkefappServiceRequest.Subject = appServiceRequest.Subject;
                chkefappServiceRequest.Status= appServiceRequest.Status;
                chkefappServiceRequest.ResolutionDate= appServiceRequest.ResolutionDate;
                chkefappServiceRequest.Priority= appServiceRequest.Priority;
                chkefappServiceRequest.Description= appServiceRequest.Description;
                chkefappServiceRequest.ModifiedBy = decryptedModifiedby;
                chkefappServiceRequest.ModifiedDate = DateTime.Now;
                chkefappServiceRequest.RequestCategoryIds= appServiceRequest.RequestCategoryIds;
                chkefappServiceRequest.UserId = decryptedUserUd;
                chkefappServiceRequest.ModifiedIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            }

            //var efappServiceRequest = this.mapper.Map<EFModel.AppServiceRequest>(appServiceRequest);

            await this.unitOfWork.AppServiceRequestRepository.UpdateAsync(chkefappServiceRequest, cancellationToken).ConfigureAwait(false);
            var AppLogin = await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(r => r.UserId == decryptedUserUd, cancellationToken).ConfigureAwait(false);
            MailRequest mailtoboard = new MailRequest();
            var emaiTemplateUser = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0011", cancellationToken).ConfigureAwait(false);
            var mdStatus= await this.unitOfWork.MdStatusRepository.FindByAsync(x => x.Id == appServiceRequest.Status, cancellationToken).ConfigureAwait(false);
            string Body = emaiTemplateUser.MessageTemplate;
            Body = Body.Replace("#Status#", mdStatus.Description);
            mailtoboard.Body = Body;
            mailtoboard.ToEmail = AppLogin.Email;
            mailtoboard.Body = Body;
            mailtoboard.Subject = emaiTemplateUser.MessageSubject;
            var mail = utilityService.SendEmailAsync(mailtoboard);
            return await this.unitOfWork.CommitAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<AppServiceRequestsList>> GetByIdAsync(getserviceRequest appServiceRequest, CancellationToken cancellationToken)
        {
            var mdsectionlist = await this.unitOfWork.MdSectionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var absmdsectionlist =this.mapper.Map<List<MdSection>>(mdsectionlist);
            var projectid = await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var status = await this.unitOfWork.MdStatusRepository.GetAllAsync(cancellationToken).ConfigureAwait(false); 
            var appServiceRequestRepository = await this.unitOfWork.AppServiceRequestRepository.FindAllByAsync(x=>x.ServiceRequestId== appServiceRequest.ServiceRequestId, cancellationToken).ConfigureAwait(false);
            var requestlistdata = from servicerequests in appServiceRequestRepository
                                  join projects in projectid on servicerequests.BoardId equals projects.ProjectId
                                
                                  select new HelpDeskAPI.Data.Abstractions.Models.AppServiceRequestsList
                                  {
                                      ServiceRequestId = servicerequests.ServiceRequestId,
                                      BoardId = (servicerequests.BoardId).ToString(),
                                      BoardName = projects.ProjectName,
                                      RequestCategoryIds = ConvertIdToCategoryName(servicerequests.RequestCategoryIds, absmdsectionlist),
                                      Subject = servicerequests.Subject,
                                      Description = servicerequests.Description,
                                      Status = (servicerequests.Status == "P") ? "Pending" : (servicerequests.Status == "A") ? "Accepted" : (servicerequests.Status == "D") ? "Discard" : (servicerequests.Status == "T") ? "Return" : (servicerequests.Status == "R") ? "Reject" : "Not Status",
                                      Priority = servicerequests.Priority,
                                      ResolutionDate = servicerequests.ResolutionDate,
                                      UserId = servicerequests.UserId,
                                      CreatedDate = servicerequests.CreatedDate,
                                      CreatedBy = servicerequests.CreatedBy,
                                      CreatedIp = servicerequests.CreatedIp,
                                      ModifiedDate = servicerequests.ModifiedDate,
                                      ModifiedBy = servicerequests.ModifiedBy,
                                      ModifiedIp = servicerequests.ModifiedIp
                                  };

            return requestlistdata.ToList();
            //return this.mapper.Map<AppServiceRequest>(appServiceRequestRepository);
        }


        /// <inheritdoc/>
        public virtual Task<Abstractions.Models.DashboardCount> GetStatusCountAsync(StatusCount statusCount, CancellationToken cancellationToken)
        {

            var param = new SqlParameter[]
           {
                new SqlParameter()
                {
                    ParameterName = "@UserId",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = statusCount.UserId,
                },
                new SqlParameter()
                {
                    ParameterName = "@mode",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = statusCount.mode,
                },

           };
            var List = new DashboardCount();
            using (var connection = unitOfWork.HelpDeskDBContext.Database.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "EXEC " + "Usp_BoardRequestCount @UserId,@mode";
                foreach (var parameterDefinition in param)
                {
                    command.Parameters.Add(new SqlParameter(parameterDefinition.ParameterName, parameterDefinition.Value));
                }
                List<Abstractions.Models.DashboardCount> countlist = new List<Abstractions.Models.DashboardCount>();
                if (statusCount.mode == "Board") { 
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countlist.Add(new Abstractions.Models.DashboardCount
                        {
                            Total = reader.IsDBNull(reader.GetOrdinal("SA")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA")),
                            Completed= reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC")),
                            Accepted = reader.IsDBNull(reader.GetOrdinal("SR")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA")),
                            Return = reader.IsDBNull(reader.GetOrdinal("ST")) ? 0 : reader.GetInt32(reader.GetOrdinal("ST")),
                            Closed= reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC")),
                            Submitted= reader.IsDBNull(reader.GetOrdinal("RS")) ? 0 : reader.GetInt32(reader.GetOrdinal("RS")),
                            Discard = reader.IsDBNull(reader.GetOrdinal("SD")) ? 0 : reader.GetInt32(reader.GetOrdinal("SD")),
                            Reject = reader.IsDBNull(reader.GetOrdinal("SR")) ? 0 : reader.GetInt32(reader.GetOrdinal("SR")),

                        });
                    }
                }

                List.StatusDetail = countlist;
                }
               else if (statusCount.mode == "BoardUser")
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countlist.Add(new Abstractions.Models.DashboardCount
                            {
                                Total = reader.IsDBNull(reader.GetOrdinal("SA")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA")),
                                Completed = reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC")),
                                Accepted = reader.IsDBNull(reader.GetOrdinal("SA")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA")),
                                Return = reader.IsDBNull(reader.GetOrdinal("ST")) ? 0 : reader.GetInt32(reader.GetOrdinal("ST")),
                                Closed = reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC")),
                                Submitted = reader.IsDBNull(reader.GetOrdinal("RS")) ? 0 : reader.GetInt32(reader.GetOrdinal("RS")),
                                Discard= reader.IsDBNull(reader.GetOrdinal("SD")) ? 0 : reader.GetInt32(reader.GetOrdinal("SD")),
                                Reject = reader.IsDBNull(reader.GetOrdinal("SR")) ? 0 : reader.GetInt32(reader.GetOrdinal("SR")),

                            });
                        }
                    }

                    List.StatusDetail = countlist;
                }
                else if(statusCount.mode == "Developer")
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countlist.Add(new Abstractions.Models.DashboardCount
                            {
                                TotalTicket = reader.IsDBNull(reader.GetOrdinal("PI")) ? 0 : reader.GetInt32(reader.GetOrdinal("PI")),
                                Progress = reader.IsDBNull(reader.GetOrdinal("PI")) ? 0 : reader.GetInt32(reader.GetOrdinal("PI")),
                                TaskClosed = reader.IsDBNull(reader.GetOrdinal("PC")) ? 0 : reader.GetInt32(reader.GetOrdinal("PC")),
                                NeedClarification = reader.IsDBNull(reader.GetOrdinal("PR")) ? 0 : reader.GetInt32(reader.GetOrdinal("PR")),
                                AlreadyCompleted = reader.IsDBNull(reader.GetOrdinal("PA")) ? 0 : reader.GetInt32(reader.GetOrdinal("PA")),
                                TkReturn = reader.IsDBNull(reader.GetOrdinal("PT")) ? 0 : reader.GetInt32(reader.GetOrdinal("PT")),
                                NeedHelp = reader.IsDBNull(reader.GetOrdinal("PN")) ? 0 : reader.GetInt32(reader.GetOrdinal("PN")),
                            });
                        }
                    }

                    List.StatusDetail = countlist;

                }
                else if (statusCount.mode == "ProjectManager")
                {
                    Abstractions.Models.DashboardCount dashboard= new Abstractions.Models.DashboardCount();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                           
                            dashboard.Progress = reader.IsDBNull(reader.GetOrdinal("PI"))?0: reader.GetInt32(reader.GetOrdinal("PI"));
                            dashboard.TaskClosed = reader.IsDBNull(reader.GetOrdinal("PC")) ? 0 : reader.GetInt32(reader.GetOrdinal("PC"));
                            dashboard.NeedClarification = reader.IsDBNull(reader.GetOrdinal("PR")) ? 0 : reader.GetInt32(reader.GetOrdinal("PR"));
                            dashboard.AlreadyCompleted = reader.IsDBNull(reader.GetOrdinal("PA")) ? 0 : reader.GetInt32(reader.GetOrdinal("PA"));
                            dashboard.TkReturn = reader.IsDBNull(reader.GetOrdinal("PT")) ? 0 : reader.GetInt32(reader.GetOrdinal("PT"));
                            dashboard.NeedHelp = reader.IsDBNull(reader.GetOrdinal("PN")) ? 0 : reader.GetInt32(reader.GetOrdinal("PN"));
                            dashboard.UnAssigned= reader.IsDBNull(reader.GetOrdinal("TU")) ? 0 : reader.GetInt32(reader.GetOrdinal("TU"));
                            dashboard.TotalTicket = dashboard.Progress + dashboard.TaskClosed+ dashboard.NeedClarification
                                + dashboard.AlreadyCompleted + dashboard.TkReturn + dashboard.NeedHelp;

                        }
                        reader.NextResult();
                        while (reader.Read())
                        {
                            dashboard.Total = (reader.IsDBNull(reader.GetOrdinal("SA")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA")))+ 
                                (reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC"))) 
                                +(reader.IsDBNull(reader.GetOrdinal("ST")) ? 0 : reader.GetInt32(reader.GetOrdinal("ST"))) 
                                +(reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC")))
                                +(reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC")));
                            dashboard.Completed = reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC"));
                            dashboard.Accepted = reader.IsDBNull(reader.GetOrdinal("SA")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA"));
                            dashboard.Return = reader.IsDBNull(reader.GetOrdinal("ST")) ? 0 : reader.GetInt32(reader.GetOrdinal("ST"));
                            dashboard.Closed = reader.IsDBNull(reader.GetOrdinal("SC")) ? 0 : reader.GetInt32(reader.GetOrdinal("SC"));
                            dashboard.Submitted = reader.IsDBNull(reader.GetOrdinal("RS")) ? 0 : reader.GetInt32(reader.GetOrdinal("RS"));
                            dashboard.Discard = reader.IsDBNull(reader.GetOrdinal("SD")) ? 0 : reader.GetInt32(reader.GetOrdinal("SD"));
                            dashboard.Reject = reader.IsDBNull(reader.GetOrdinal("SR")) ? 0 : reader.GetInt32(reader.GetOrdinal("SR"));
                        }
                        countlist.Add(dashboard);
                    }

                    List.StatusDetail = countlist;

                }
            }

            return Task.FromResult(List);
        }

        public virtual async Task<string> SendOTP(OTPModal otpModal, CancellationToken cancellationToken)
        {
            string userId = decryptionService.Decryption(otpModal.UserId);
            string decryptedOTPsms = decryptionService.Decryption(otpModal.OtpSms);

            string response = "";
            var smsTemplateOTP = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0001", cancellationToken).ConfigureAwait(false);

            string isdCode = "91";           
            string TemplateId = smsTemplateOTP.RegisteredTemplateId;
            string message = smsTemplateOTP.MessageTemplate;
            message = message.Replace("#OTP#", decryptedOTPsms);
            message = message.Replace("#EMAIL#", "counselling-pmu@nic.in");
            if(decryptedOTPsms != "NA")
            {
                var apploginDetail = await this.unitOfWork.AppLoginDetailsRepository.FindByAsync(x => x.UserId== userId, cancellationToken).ConfigureAwait(false);
                
                if(apploginDetail != null)
                {
                    string contactNumber = (apploginDetail.Mobile).Trim();
                   var sms = sMSService.SendAsync(isdCode + contactNumber, decryptedOTPsms, TemplateId, message);
                    response = "OTP has been sent successfully";
                }
                else
                {
                    response = "Please Enter Valid User Id";
                }                
            }
            else
            {
                response = "OTP not send";
                return response;
            }
            return response;

        }
        public virtual async Task<string> GetIPAddress(CancellationToken cancellationToken)
        {
            var refreshtoken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            return ip.ToString();

        }

        public string ConvertIdToCategoryName(string id, List<AbsModels.MdSection> mdsectionlist)
        {
            try
            {
                string[] categoryIds = id.Split(',');
                string categoryName = "";
                //var mdsectionlist = this.unitOfWork.MdSectionRepository.GetAll();
                //return this.mapper.Map<List<AbsModels.MdSection>>(mdsectionlist);

                var data = from Ids in categoryIds
                           join mdsection in mdsectionlist on Ids equals mdsection.SectionId.ToString()
                           select new MdSection
                           {
                               SectionId = mdsection.SectionId,
                               Section = mdsection.Section,
                               IsActive = mdsection.IsActive
                           };
                var selectedCategories = data.ToArray();
                for (int i = 0; i < selectedCategories.Length; i++)
                {
                    if (i == 0)
                    {
                        categoryName = selectedCategories[i].Section;
                    }
                    else
                    {
                        categoryName = categoryName + "," + selectedCategories[i].Section;
                    }

                }
                return categoryName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}