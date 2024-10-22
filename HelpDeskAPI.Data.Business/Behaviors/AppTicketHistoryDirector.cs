
//-----------------------------------------------------------------------
// <copyright file="AppTicketHistoryDirector.cs" company="NIC">
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

    /// <inheritdoc />
    public class AppTicketHistoryDirector : IAppTicketHistoryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppTicketHistoryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppTicketHistoryDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppTicketHistory>> GetAllAsync(CancellationToken cancellationToken)
        {
            var apptickethistorylist = await this.unitOfWork.AppTicketHistoryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppTicketHistory>>(apptickethistorylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.AppTicketHistory> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
            var apptickethistorylist = await this.unitOfWork.AppTicketHistoryRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppTicketHistory>(apptickethistorylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.AppTicketHistory apptickethistory, CancellationToken cancellationToken)
        {
            if (apptickethistory == null)
            {
                throw new ArgumentNullException(nameof(apptickethistory));
            }

            var chkefapptickethistory = await this.unitOfWork.AppTicketHistoryRepository.FindByAsync(r => r.Id == apptickethistory.Id, default);
            if (chkefapptickethistory != null)
            {
                throw new EntityFoundException($"This Records {chkefapptickethistory} already exists");
            }

            var efapptickethistory = this.mapper.Map<Data.EF.Models.AppTicketHistory>(apptickethistory);

            await this.unitOfWork.AppTicketHistoryRepository.InsertAsync(efapptickethistory, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.AppTicketHistory apptickethistory, CancellationToken cancellationToken)
        
		{
            if (apptickethistory.Id == 0)
            {
                throw new ArgumentException(nameof(apptickethistory.Id));
            }
			
			Data.EF.Models.AppTicketHistory entityUpd = await unitOfWork.AppTicketHistoryRepository.FindByAsync(e => e.Id == apptickethistory.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= apptickethistory.Id;
					entityUpd.TicketId= apptickethistory.TicketId;
					entityUpd.TicketNo= apptickethistory.TicketNo;
					entityUpd.ServiceRequestId= apptickethistory.ServiceRequestId;
					entityUpd.BoardId= apptickethistory.BoardId;
					entityUpd.RequestCategoryId= apptickethistory.RequestCategoryId;
					entityUpd.Subject= apptickethistory.Subject;
					entityUpd.Description= apptickethistory.Description;
					entityUpd.Remarks= apptickethistory.Remarks;
					entityUpd.AssignStatus= apptickethistory.AssignStatus;
					entityUpd.TaskStatus= apptickethistory.TaskStatus;
					entityUpd.Priority= apptickethistory.Priority;
					entityUpd.UserId= apptickethistory.UserId;
					entityUpd.AssignTo= apptickethistory.AssignTo;
					entityUpd.StartDate= apptickethistory.StartDate;
					entityUpd.EndDate= apptickethistory.EndDate;
					entityUpd.CreatedDate= apptickethistory.CreatedDate;
					entityUpd.CreatedBy= apptickethistory.CreatedBy;
					entityUpd.CreatedIp= apptickethistory.CreatedIp;
					entityUpd.ModifiedDate= apptickethistory.ModifiedDate;
					entityUpd.ModifiedBy= apptickethistory.ModifiedBy;
					entityUpd.ModifiedIp= apptickethistory.ModifiedIp;
					entityUpd.InsertedOn= apptickethistory.InsertedOn;
					
				await unitOfWork.AppTicketHistoryRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.AppTicketHistoryRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.AppTicketHistoryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppTicketHistoryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	