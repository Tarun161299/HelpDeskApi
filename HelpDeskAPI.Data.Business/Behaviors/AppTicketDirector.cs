
//-----------------------------------------------------------------------
// <copyright file="AppTicketDirector.cs" company="NIC">
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
    using HelpDeskAPI.Data.Abstractions.Models;
    using static System.Collections.Specialized.BitVector32;
    using System.Text;
    using System.Text.Json;
    using HelpDeskAPI.Data.EF.Models;

    /// <inheritdoc />
    public class AppTicketDirector : IAppTicketDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppTicketDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppTicketDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.GetTicketListByID>> GetAllAsync(GetTicketByUserAndStatus userIdAndStatus, CancellationToken cancellationToken)
        {//MdSectionRepository
            var projectid = await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false); ;
            var appticketlist = await this.unitOfWork.AppTicketRepository.GetAllAsync( cancellationToken).ConfigureAwait(false);
            var mdSection = await this.unitOfWork.MdSectionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdPriority= await this.unitOfWork.MdPriorityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdStatus = await this.unitOfWork.MdStatusRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdUserRoleBoardMapping = await this.unitOfWork.MdUserBoardRoleMappingRepository.FindAllByAsync(x=>x.UserId== userIdAndStatus.userId, cancellationToken).ConfigureAwait(false);
            var requestlistdata = from ticketlist in appticketlist
                                  join projects in projectid on ticketlist.BoardId equals projects.ProjectId
                                  join  section in mdSection on ticketlist.SectionId equals section.SectionId
                                  join status in mdStatus on ticketlist.TaskStatus equals status.Id
                                  join priority in mdPriority on ticketlist.Priority equals (priority.PriorityId).ToString()
                                  join roleMapping in mdUserRoleBoardMapping on ticketlist.BoardId equals roleMapping.BoardId
                                  select new HelpDeskAPI.Data.Abstractions.Models.GetTicketListByID

                                  {
                                      TicketId = ticketlist.TicketId,
                                      TicketNo = ticketlist.TicketNo,
                                      ServiceRequestId = ticketlist.ServiceRequestId,
                                      BoardId = ticketlist.BoardId,
                                      BoardName = projects.ProjectName,
                                      SectionDescription= section.Section,
                                      SectionId = ticketlist.SectionId,
                                      Subject = ticketlist.Subject,
                                      Description = ticketlist.Description,
                                      AssignStatus = ticketlist.AssignStatus,
                                      TaskStatus = ticketlist.TaskStatus,
                                      TaskStatusDescription= status.Description,
                                      Priority = ticketlist.Priority,
                                      PriorityDescription= priority.Description,
                                      AssignTo = ticketlist.AssignTo,
                                      StartDate = ticketlist.StartDate,
                                      EndDate = ticketlist.EndDate,
                                      CreatedDate = ticketlist.CreatedDate,
                                      CreatedBy = ticketlist.CreatedBy,
                                      CreatedIp = ticketlist.CreatedIp,
                                      ModifiedDate = ticketlist.ModifiedDate,
                                      ModifiedBy = ticketlist.ModifiedBy,
                                      ModifiedIp = ticketlist.ModifiedIp,
                                  };

            // var result = this.mapper.Map<Abstractions.Models.AppTicket>(appticketlist);
            if(userIdAndStatus.status== "TA" || userIdAndStatus.status == "TU")
             {
                var TicketAssigned=from ticketAssigned in requestlistdata where ticketAssigned.AssignStatus== userIdAndStatus.status
                                   select new HelpDeskAPI.Data.Abstractions.Models.GetTicketListByID
                                   {
                                       TicketId = ticketAssigned.TicketId,
                                       TicketNo = ticketAssigned.TicketNo,
                                       ServiceRequestId = ticketAssigned.ServiceRequestId,
                                       BoardId = ticketAssigned.BoardId,
                                       BoardName = ticketAssigned.BoardName,
                                       SectionDescription = ticketAssigned.SectionDescription,
                                       SectionId = ticketAssigned.SectionId,
                                       Subject = ticketAssigned.Subject,
                                       Description = ticketAssigned.Description,
                                       AssignStatus = ticketAssigned.AssignStatus,
                                       TaskStatus = ticketAssigned.TaskStatus,
                                       TaskStatusDescription = ticketAssigned.Description,
                                       Priority = ticketAssigned.Priority,
                                       PriorityDescription = ticketAssigned.Description,
                                       AssignTo = ticketAssigned.AssignTo,
                                       StartDate = ticketAssigned.StartDate,
                                       EndDate = ticketAssigned.EndDate,
                                       CreatedDate = ticketAssigned.CreatedDate,
                                       CreatedBy = ticketAssigned.CreatedBy,
                                       CreatedIp = ticketAssigned.CreatedIp,
                                       ModifiedDate = ticketAssigned.ModifiedDate,
                                       ModifiedBy = ticketAssigned.ModifiedBy,
                                       ModifiedIp = ticketAssigned.ModifiedIp,
                                   };
                //return TicketAssigned.ToList();
                return TicketAssigned.OrderByDescending(rs => rs.TicketId).ToList();
                 }
           
                if (userIdAndStatus.status == "PI" || userIdAndStatus.status == "PC" || userIdAndStatus.status == "PR" || userIdAndStatus.status == "PN"
                || userIdAndStatus.status == "PA" || userIdAndStatus.status == "PT")
                {
                    var TicketAssigned = from ticketAssigned in requestlistdata
                                         where ticketAssigned.TaskStatus == userIdAndStatus.status
                                         select new HelpDeskAPI.Data.Abstractions.Models.GetTicketListByID
                                         {
                                             TicketId = ticketAssigned.TicketId,
                                             TicketNo = ticketAssigned.TicketNo,
                                             ServiceRequestId = ticketAssigned.ServiceRequestId,
                                             BoardId = ticketAssigned.BoardId,
                                             BoardName = ticketAssigned.BoardName,
                                             SectionDescription = ticketAssigned.SectionDescription,
                                             SectionId = ticketAssigned.SectionId,
                                             Subject = ticketAssigned.Subject,
                                             Description = ticketAssigned.Description,
                                             AssignStatus = ticketAssigned.AssignStatus,
                                             TaskStatus = ticketAssigned.TaskStatus,
                                             TaskStatusDescription = ticketAssigned.Description,
                                             Priority = ticketAssigned.Priority,
                                             PriorityDescription = ticketAssigned.Description,
                                             AssignTo = ticketAssigned.AssignTo,
                                             StartDate = ticketAssigned.StartDate,
                                             EndDate = ticketAssigned.EndDate,
                                             CreatedDate = ticketAssigned.CreatedDate,
                                             CreatedBy = ticketAssigned.CreatedBy,
                                             CreatedIp = ticketAssigned.CreatedIp,
                                             ModifiedDate = ticketAssigned.ModifiedDate,
                                             ModifiedBy = ticketAssigned.ModifiedBy,
                                             ModifiedIp = ticketAssigned.ModifiedIp,
                                         };
                    //return TicketAssigned.ToList();
                return TicketAssigned.OrderByDescending(rs => rs.TicketId).ToList();

            }
            //return requestlistdata.ToList();
            return requestlistdata.OrderByDescending(rs => rs.TicketId).ToList();
        }

		/// <inheritdoc/>
        public virtual async Task<List<AbsModels.GetTicketListByID>> GetByIdAsync(int TicketId, CancellationToken cancellationToken)
        {
            var projectid = await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdStatus = await this.unitOfWork.MdStatusRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdSection = await this.unitOfWork.MdSectionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdPriority = await this.unitOfWork.MdPriorityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var appticketlist = await this.unitOfWork.AppTicketRepository.FindAllByAsync(x => x.TicketId == TicketId, cancellationToken).ConfigureAwait(false);
            var AppserviceRequest= await this.unitOfWork.AppServiceRequestRepository.GetAllAsync(cancellationToken).ConfigureAwait(false); ;
            var requestlistdata = from ticketlist in appticketlist
                                  join projects in projectid on ticketlist.BoardId equals projects.ProjectId
                                  join status in mdStatus on ticketlist.TaskStatus equals status.Id
                                  join section in mdSection on ticketlist.SectionId equals section.SectionId
                                  join priority in mdPriority on ticketlist.Priority equals (priority.PriorityId).ToString()
                                  join serviceRequest in AppserviceRequest on ticketlist.ServiceRequestId equals serviceRequest.ServiceRequestId
                                  select new HelpDeskAPI.Data.Abstractions.Models.GetTicketListByID
                                  {TicketId=ticketlist.TicketId,
                                      TicketNo=ticketlist.TicketNo,
                                      ServiceRequestId=ticketlist.ServiceRequestId,
                                      BoardId = ticketlist.BoardId,
                                      BoardName = projects.ProjectName,
                                      SectionId = ticketlist.SectionId,
                                      FileID=ticketlist.FileId,
                                      Subject = ticketlist.Subject,
                                      Description= ticketlist.Description,
                                      SectionDescription = section.Section,
                                      AssignStatus = ticketlist.AssignStatus,
                                      TaskStatusDescription = status.Description,
                                      PriorityDescription = priority.Description,
                                      TaskStatus = ticketlist.TaskStatus,
                                      Priority= ticketlist.Priority,
                                      AssignTo= ticketlist.AssignTo,
                                      StartDate= ticketlist.StartDate,
                                      EndDate= ticketlist.EndDate,
                                      CreatedDate= ticketlist.CreatedDate,
                                      CreatedBy= ticketlist.CreatedBy,
                                      CreatedIp= ticketlist.CreatedIp,
                                      ModifiedDate= ticketlist.ModifiedDate,
                                      ModifiedBy= ticketlist.ModifiedBy,
                                      ModifiedIp= ticketlist.ModifiedIp,
                                      ServiceRequestSubject= serviceRequest.Subject,
                                      ServiceRequestNo= serviceRequest.ServiceRequestNo
                                  };
           // var result = this.mapper.Map<Abstractions.Models.AppTicket>(appticketlist);
            return requestlistdata.ToList();
        }

		/// <inheritdoc/>
        public async Task<bool> InsertAsync(AbsModels.GenerateTicket appticket, CancellationToken cancellationToken)
        {

            var jsonString = JsonSerializer.Serialize(appticket);
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
                    ParameterName = "@IsError",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output,
                },
                //new SqlParameter()
                //{
                //    ParameterName = "@Message",   
                //    SqlDbType = System.Data.SqlDbType.VarChar,
                //    Direction = System.Data.ParameterDirection.Output,
                //},
            };
            var storedProcedureName = $"{"Sp_GenerateTicket"}  @InputJson,@IsError output";
            int result = await this.unitOfWork.AppTicketRepository.ExecuteSqlRawAsync(storedProcedureName, ref param, cancellationToken).ConfigureAwait(false);
            bool s = (bool)param[1].Value;
            return s;
            //if (appticket == null)
            //{
            //    throw new ArgumentNullException(nameof(appticket));
            //}
            //var maxid= await this.unitOfWork.AppTicketRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            //var id= this.mapper.Map<List<AbsModels.AppTicket>>(maxid).OrderByDescending(u => u.TicketId).FirstOrDefault();
            //appticket.TicketId = id.TicketId + 1;

            //var chkefappticket = await this.unitOfWork.AppTicketRepository.FindByAsync(r => r.TicketId == appticket.TicketId, default);
            //if (chkefappticket != null)
            //{
            //    throw new EntityFoundException($"This Records {chkefappticket} already exists");
            //}

            //var efappticket = this.mapper.Map<Data.EF.Models.AppTicket>(appticket);

            //await this.unitOfWork.AppTicketRepository.InsertAsync(efappticket, cancellationToken).ConfigureAwait(false);
            //return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.AppTicket appticket, CancellationToken cancellationToken)
        
		{
            if (appticket.TicketId == 0)
            {
                throw new ArgumentException(nameof(appticket.TicketId));
            }
			
			Data.EF.Models.AppTicket entityUpd = await unitOfWork.AppTicketRepository.FindByAsync(e => e.TicketId == appticket.TicketId, cancellationToken);
			if (entityUpd != null)
            {
			//entityUpd.TicketId= appticket.TicketId;
					entityUpd.TicketNo= appticket.TicketNo;
					entityUpd.ServiceRequestId= appticket.ServiceRequestId;
					entityUpd.BoardId= appticket.BoardId;
                    entityUpd.FileId= appticket.FileId;

                    entityUpd.SectionId= appticket.SectionId;
					entityUpd.Subject= appticket.Subject;
					entityUpd.Description= appticket.Description;
					entityUpd.AssignStatus= appticket.AssignStatus;
					entityUpd.TaskStatus= appticket.TaskStatus;
					entityUpd.Priority= appticket.Priority;
					entityUpd.AssignTo= appticket.AssignTo;
					entityUpd.StartDate= appticket.StartDate;
					entityUpd.EndDate= appticket.EndDate;
					entityUpd.CreatedDate= appticket.CreatedDate;
					entityUpd.CreatedBy= appticket.CreatedBy;
					entityUpd.CreatedIp= appticket.CreatedIp;
					entityUpd.ModifiedDate= appticket.ModifiedDate;
					entityUpd.ModifiedBy= appticket.ModifiedBy;
					entityUpd.ModifiedIp= appticket.ModifiedIp;
					
				await unitOfWork.AppTicketRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int TicketId, CancellationToken cancellationToken)
        {
            if (TicketId == 0)
            {
                throw new ArgumentNullException(nameof(TicketId));
            }

            var entity = await this.unitOfWork.AppTicketRepository.FindByAsync(x => x.TicketId == TicketId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an TicketId {TicketId} was not found.");
            }

            await this.unitOfWork.AppTicketRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppTicketRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.AppTicket>> GetByBoardAsync(GetTicketbyService getTicketByService, CancellationToken cancellationToken)
        {
            var appticketlist = await this.unitOfWork.AppTicketRepository.FindAllByAsync(x => x.BoardId == getTicketByService.boardId && x.ServiceRequestId== getTicketByService.serviceRequestId, cancellationToken).ConfigureAwait(false);
            var mdSectionList = await this.unitOfWork.MdSectionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdStatusList = await this.unitOfWork.MdStatusRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

            var result = from appticket in appticketlist
                         join section in mdSectionList on appticket.SectionId equals section.SectionId
                         join statusList in mdStatusList on appticket.TaskStatus equals statusList.Id
                         where appticket.ServiceRequestId == getTicketByService.serviceRequestId
                         select new HelpDeskAPI.Data.Abstractions.Models.AppTicket
                         {
                             TicketId = appticket.TicketId,
                             TicketNo = appticket.TicketNo,
                             ServiceRequestId = appticket.ServiceRequestId,
                             BoardId = appticket.BoardId,
                             SectionId = appticket.SectionId,
                             Subject = appticket.Subject,
                             AssignStatus = appticket.AssignStatus,
                             TaskStatus = appticket.TaskStatus, 
                             Priority = appticket.Priority,
                             AssignTo = appticket.AssignTo,
                             StartDate = appticket.StartDate,
                             EndDate = appticket.EndDate,
                             CreatedDate = appticket.CreatedDate,
                             CreatedBy = appticket.CreatedBy,
                             CreatedIp= appticket.CreatedIp,
                             ModifiedDate = appticket.ModifiedDate,
                             ModifiedBy = appticket.ModifiedBy,
                             ModifiedIp = appticket.ModifiedIp,
                             Section = section.Section,
                             Description = statusList.Description,


                         };

            var finalresult = this.mapper.Map<List<Abstractions.Models.AppTicket>>(result);
            return finalresult.ToList();

        }
    }
	}
	