//-----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using HelpDeskAPI.Data.EF.Models;

    /// <summary>
    /// Interface for Unit of Work.
    /// </summary>
    public interface IUnitOfWork
    {

        /// <summary>
        /// Gets OBSDBContext Propery.
        /// </summary>
        HelpDeskDBContext HelpDeskDBContext { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Number of rows.</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets AppTicket List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppTicket> AppTicketRepository { get; }

        /// <summary>
        /// Gets AppTicketHistory List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppTicketHistory> AppTicketHistoryRepository { get; }

        /// <summary>
        /// Gets AppServiceRequestHistory List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppServiceRequestHistory> AppServiceRequestHistoryRepository { get; }

        /// <summary>
        /// Gets AppRemarks List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppRemarks> AppRemarksRepository { get; }

        /// <summary>
        /// Gets AppRoleModulePermission List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppRoleModulePermission> AppRoleModulePermissionRepository { get; }

        /// <summary>
        /// Gets MdUserBoardMapping List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdUserBoardMapping> MdUserBoardMappingRepository { get; }

        /// <summary>
        /// Gets MdUserBoardRoleMapping List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdUserBoardRoleMapping> MdUserBoardRoleMappingRepository { get; }

        /// <summary>
        /// Gets AppDocumentUploadedDetail List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppDocumentUploadedDetail> AppDocumentUploadedDetailRepository { get; }

        /// <summary>
        /// Gets AppLoginDetails List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppLoginDetails> AppLoginDetailsRepository { get; }

        /// <summary>
        /// Gets MdActionType List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdActionType> MdActionTypeRepository { get; }

        /// <summary>
        /// Gets MdAgency List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdAgency> MdAgencyRepository { get; }

        /// <summary>
        /// Gets MdDocumentType List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdDocumentType> MdDocumentTypeRepository { get; }

        /// <summary>
        /// Gets MdModule List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdModule> MdModuleRepository { get; }

        /// <summary>
        /// Gets MdRole List Property.
        /// </summary>
        IGenericRepository<EF.Models.MdRole> MdRoleRepository { get; }

        /// <summary>
        /// Gets MdSection List Property.
        /// </summary>
        IGenericRepository<EF.Models.AppServiceRequest> AppServiceRequestRepository { get; }
        
        IGenericRepository<EF.Models.MdSection> MdSectionRepository { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        IGenericRepository<EF.Models.ZmstProjects> ZmstProjectsRepository { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        IGenericRepository<EF.Models.MdSmsEmailTemplate> MdSmsEmailTemplateRepository { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        IGenericRepository<EF.Models.MdStatus> MdStatusRepository { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        IGenericRepository<EF.Models.MdPriority> MdPriorityRepository { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        IGenericRepository<EF.Models.Esodata> EsodataRepository { get; }

        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        IGenericRepository<EF.Models.AppCaptcha> AppCaptchaRepository { get; }
        /// <summary>
        /// Commits all work to data store.
        /// </summary>
        IGenericRepository<EF.Models.UserAuthorization> UserAuthorizationRepository { get; }
    }
}