//-----------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data
{
    using System.Threading;
    using System.Threading.Tasks;
    using HelpDeskAPI.Data.Interfaces;
    using HelpDeskAPI.Data.EF.Models;
   

    //using CommonModels = HelpDeskAPI.Common.Models;

    /// <inheritdoc/>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HelpDeskDBContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="oBSDBContext">Onboarding System DbContext.</param>

        /// <param name="mdAgencyRepository">mdAgencyRepository Repository.</param>
        /// <param name="appservicerequestRepository">appservicerequestRepository Repository.</param>
        /// <param name="appticketRepository">appticketRepository Repository.</param>
        /// <param name="apptickethistoryRepository">apptickethistoryRepository Repository.</param>
        /// <param name="appservicerequesthistoryRepository">appservicerequesthistoryRepository Repository.</param>
        /// <param name="appremarksRepository">appremarksRepository Repository.</param>
        /// <param name="approlemodulepermissionRepository">approlemodulepermissionRepository Repository.</param>
        /// <param name="mduserboardmappingRepository">mduserboardmappingRepository Repository.</param>
        /// <param name="mduserboardrolemappingRepository">mduserboardrolemappingRepository Repository.</param>
        /// <param name="appdocumentuploadeddetailRepository">appdocumentuploadeddetailRepository Repository.</param>
        /// <param name="applogindetailsRepository">applogindetailsRepository Repository.</param>
        /// <param name="mdactiontypeRepository">mdactiontypeRepository Repository.</param>
        /// <param name="mdagencyRepository">mdagencyRepository Repository.</param>
        /// <param name="mddocumenttypeRepository">mddocumenttypeRepository Repository.</param>
        /// <param name="mdmoduleRepository">mdmoduleRepository Repository.</param>
        /// <param name="mdroleRepository">mdroleRepository Repository.</param>
        /// <param name="mdsectionRepository">mdsectionRepository Repository.</param>
        /// <param name="zmstprojectsRepository">zmstprojectsRepository Repository.</param>
        /// <param name="zmstprojectsRepository">zmstprojectsRepository Repository.</param>
        /// <param name="appcaotchaRepository">appcaotchaRepository Repository.</param>
        /// <param name="userAuthorizationRepository">userAuthorization Repository.</param>

        public UnitOfWork(
            HelpDeskDBContext objHelpDeskDBContext,
            IGenericRepository<MdAgency> mdAgencyRepository,
             IGenericRepository<AppServiceRequest> appServiceRequestRepository,
                   
                    IGenericRepository<AppServiceRequest> appservicerequestRepository,
                    IGenericRepository<AppTicket> appticketRepository,
                    IGenericRepository<AppTicketHistory> apptickethistoryRepository,
                    IGenericRepository<AppServiceRequestHistory> appservicerequesthistoryRepository,
                    IGenericRepository<AppRemarks> appremarksRepository,
                    IGenericRepository<AppRoleModulePermission> approlemodulepermissionRepository,
                    IGenericRepository<MdUserBoardMapping> mduserboardmappingRepository,
                    IGenericRepository<MdUserBoardRoleMapping> mduserboardrolemappingRepository,
                    IGenericRepository<AppDocumentUploadedDetail> appdocumentuploadeddetailRepository,
                    IGenericRepository<AppLoginDetails> applogindetailsRepository,
                    IGenericRepository<MdActionType> mdactiontypeRepository,
                    IGenericRepository<MdAgency> mdagencyRepository,
                    IGenericRepository<MdDocumentType> mddocumenttypeRepository,
                    IGenericRepository<MdModule> mdmoduleRepository,
                    IGenericRepository<MdRole> mdroleRepository,
                    IGenericRepository<MdSection> mdsectionRepository,
                    IGenericRepository<ZmstProjects> zmstprojectsRepository,
                    IGenericRepository<MdSmsEmailTemplate> mdSmsEmailTemplateRepository,
                    IGenericRepository<MdStatus> mdStatusRepository,
                    IGenericRepository<MdPriority> mdPriorityRepository,
                    IGenericRepository<Esodata> esodataRepository,
			        IGenericRepository<AppCaptcha> appCaptchaRepository,
                    IGenericRepository<UserAuthorization> userAuthorizationRepository

            )
        {
            this.dbContext = objHelpDeskDBContext;
            this.HelpDeskDBContext = objHelpDeskDBContext;
            this.MdAgencyRepository = mdAgencyRepository;
            this.AppServiceRequestRepository = appservicerequestRepository;
            this.AppTicketRepository = appticketRepository;
            this.AppTicketHistoryRepository = apptickethistoryRepository;
            this.AppServiceRequestHistoryRepository = appservicerequesthistoryRepository;
            this.AppRemarksRepository = appremarksRepository;
            this.AppRoleModulePermissionRepository = approlemodulepermissionRepository;
            this.MdUserBoardMappingRepository = mduserboardmappingRepository; 
            this.MdUserBoardRoleMappingRepository = mduserboardrolemappingRepository;
            this.AppDocumentUploadedDetailRepository = appdocumentuploadeddetailRepository;
            this.AppLoginDetailsRepository = applogindetailsRepository;
            this.MdActionTypeRepository = mdactiontypeRepository;
            this.MdAgencyRepository = mdagencyRepository;
            this.MdDocumentTypeRepository = mddocumenttypeRepository;
            this.MdModuleRepository = mdmoduleRepository;
            this.MdRoleRepository = mdroleRepository;
            this.MdSectionRepository = mdsectionRepository;
            this.ZmstProjectsRepository = zmstprojectsRepository;
            this.MdSmsEmailTemplateRepository = mdSmsEmailTemplateRepository;
            this.MdStatusRepository = mdStatusRepository;
            this.MdPriorityRepository = mdPriorityRepository;
            this.EsodataRepository = esodataRepository;
			this.AppCaptchaRepository = appCaptchaRepository;
            this.UserAuthorizationRepository = userAuthorizationRepository;

        }

        /// <inheritdoc/>
        public HelpDeskDBContext HelpDeskDBContext { get; }

        /// <inheritdoc/>
        public virtual Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return this.dbContext.SaveChangesAsync(cancellationToken);
        }
        /// <inheritdoc/>
        public IGenericRepository<AppServiceRequest> AppServiceRequestRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppTicket> AppTicketRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppTicketHistory> AppTicketHistoryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppServiceRequestHistory> AppServiceRequestHistoryRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppRemarks> AppRemarksRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppRoleModulePermission> AppRoleModulePermissionRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdUserBoardMapping> MdUserBoardMappingRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdUserBoardRoleMapping> MdUserBoardRoleMappingRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppDocumentUploadedDetail> AppDocumentUploadedDetailRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppLoginDetails> AppLoginDetailsRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdActionType> MdActionTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdAgency> MdAgencyRepository { get; }
        
        /// <inheritdoc/>
        public IGenericRepository<MdDocumentType> MdDocumentTypeRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdModule> MdModuleRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdRole> MdRoleRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdSection> MdSectionRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<ZmstProjects> ZmstProjectsRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<MdSmsEmailTemplate> MdSmsEmailTemplateRepository { get; }
        
        /// <inheritdoc/>
        public IGenericRepository<MdStatus> MdStatusRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<EF.Models.MdPriority> MdPriorityRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<EF.Models.Esodata> EsodataRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<AppCaptcha> AppCaptchaRepository { get; }

        /// <inheritdoc/>
        public IGenericRepository<UserAuthorization> UserAuthorizationRepository { get; }
    }
}