//-----------------------------------------------------------------------
// <copyright file="MappingProfile.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace HelpDeskAPI.Data.Business
{
    using AutoMapper;
    using AbsModels = HelpDeskAPI.Data.Abstractions.Models;
    //using CommonModels = HelpDeskAPI.Common.Models;

    /// <summary>
    /// Automapper configuration.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            OnBoardingMapping();
        }

        /// <summary>
        /// MdMinistry Mapping.
        /// </summary>
        private void OnBoardingMapping()
        {
			CreateMap<EF.Models.AppServiceRequest, AbsModels.AppServiceRequest>();
			CreateMap<AbsModels.AppServiceRequest, EF.Models.AppServiceRequest>();

			CreateMap<EF.Models.AppTicket, AbsModels.AppTicket>();
			CreateMap<AbsModels.AppTicket, EF.Models.AppTicket>();

			CreateMap<EF.Models.AppTicketHistory, AbsModels.AppTicketHistory>();
			CreateMap<AbsModels.AppTicketHistory, EF.Models.AppTicketHistory>();

			CreateMap<EF.Models.AppServiceRequestHistory, AbsModels.AppServiceRequestHistory>();
			CreateMap<AbsModels.AppServiceRequestHistory, EF.Models.AppServiceRequestHistory>();

			CreateMap<EF.Models.AppRemarks, AbsModels.AppRemarks>();
			CreateMap<AbsModels.AppRemarks, EF.Models.AppRemarks>();

			CreateMap<EF.Models.AppRoleModulePermission, AbsModels.AppRoleModulePermission>();
			CreateMap<AbsModels.AppRoleModulePermission, EF.Models.AppRoleModulePermission>();

			CreateMap<EF.Models.MdUserBoardMapping, AbsModels.MdUserBoardMapping>();
			CreateMap<AbsModels.MdUserBoardMapping, EF.Models.MdUserBoardMapping>();

			CreateMap<EF.Models.MdUserBoardRoleMapping, AbsModels.MdUserBoardRoleMapping>();
			CreateMap<AbsModels.MdUserBoardRoleMapping, EF.Models.MdUserBoardRoleMapping>();

			CreateMap<EF.Models.AppDocumentUploadedDetail, AbsModels.AppDocumentUploadedDetail>();
			CreateMap<AbsModels.AppDocumentUploadedDetail, EF.Models.AppDocumentUploadedDetail>();

			CreateMap<EF.Models.AppLoginDetails, AbsModels.AppLoginDetails>();
			CreateMap<AbsModels.AppLoginDetails, EF.Models.AppLoginDetails>();

			CreateMap<EF.Models.MdActionType, AbsModels.MdActionType>();
			CreateMap<AbsModels.MdActionType, EF.Models.MdActionType>();

			CreateMap<EF.Models.MdAgency, AbsModels.MdAgency>();
			CreateMap<AbsModels.MdAgency, EF.Models.MdAgency>();

			CreateMap<EF.Models.MdDocumentType, AbsModels.MdDocumentType>();
			CreateMap<AbsModels.MdDocumentType, EF.Models.MdDocumentType>();

			CreateMap<EF.Models.MdModule, AbsModels.MdModule>();
			CreateMap<AbsModels.MdModule, EF.Models.MdModule>();

			CreateMap<EF.Models.MdRole, AbsModels.MdRole>();
			CreateMap<AbsModels.MdRole, EF.Models.MdRole>();

			CreateMap<EF.Models.MdSection, AbsModels.MdSection>();
			CreateMap<AbsModels.MdSection, EF.Models.MdSection>();

			CreateMap<EF.Models.ZmstProjects, AbsModels.ZmstProjects>();
			CreateMap<AbsModels.ZmstProjects, EF.Models.ZmstProjects>();

            CreateMap<EF.Models.MdSmsEmailTemplate, AbsModels.MdSmsEmailTemplate>();
            CreateMap<AbsModels.MdSmsEmailTemplate, EF.Models.MdSmsEmailTemplate>();
        

            CreateMap<EF.Models.MdStatus, AbsModels.MD_Status>();
            CreateMap<AbsModels.MD_Status, EF.Models.MdStatus>();

            CreateMap<EF.Models.MdPriority, AbsModels.MD_priority>();
            CreateMap<AbsModels.MD_priority, EF.Models.MdPriority>();

            CreateMap<EF.Models.Esodata, AbsModels.Esodata>();
            CreateMap<AbsModels.Esodata, EF.Models.Esodata>();

			CreateMap<EF.Models.AppCaptcha, AbsModels.AppCaptcha>();
			CreateMap<AbsModels.AppCaptcha, EF.Models.AppCaptcha>();

            CreateMap<EF.Models.UserAuthorization, AbsModels.UserAuthorization>();
            CreateMap<AbsModels.UserAuthorization, EF.Models.UserAuthorization>();

            CreateMap<EF.Models.LogActivityAuditTrail, AbsModels.LogActivityAuditTrail>();
            CreateMap<AbsModels.LogActivityAuditTrail, EF.Models.LogActivityAuditTrail>();
            
        }
    }
}