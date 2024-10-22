
//-----------------------------------------------------------------------
// <copyright file="AppServiceRequestHistoryDirector.cs" company="NIC">
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
    public class AppServiceRequestHistoryDirector : IAppServiceRequestHistoryDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceRequestHistoryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppServiceRequestHistoryDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppServiceRequestHistory>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appservicerequesthistorylist = await this.unitOfWork.AppServiceRequestHistoryRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppServiceRequestHistory>>(appservicerequesthistorylist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.AppServiceRequestHistory> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
            var appservicerequesthistorylist = await this.unitOfWork.AppServiceRequestHistoryRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppServiceRequestHistory>(appservicerequesthistorylist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.AppServiceRequestHistory appservicerequesthistory, CancellationToken cancellationToken)
        {
            if (appservicerequesthistory == null)
            {
                throw new ArgumentNullException(nameof(appservicerequesthistory));
            }

            var chkefappservicerequesthistory = await this.unitOfWork.AppServiceRequestHistoryRepository.FindByAsync(r => r.Id == appservicerequesthistory.Id, default);
            if (chkefappservicerequesthistory != null)
            {
                throw new EntityFoundException($"This Records {chkefappservicerequesthistory} already exists");
            }

            var efappservicerequesthistory = this.mapper.Map<Data.EF.Models.AppServiceRequestHistory>(appservicerequesthistory);

            await this.unitOfWork.AppServiceRequestHistoryRepository.InsertAsync(efappservicerequesthistory, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.AppServiceRequestHistory appservicerequesthistory, CancellationToken cancellationToken)
        
		{
            if (appservicerequesthistory.Id == 0)
            {
                throw new ArgumentException(nameof(appservicerequesthistory.Id));
            }
			
			Data.EF.Models.AppServiceRequestHistory entityUpd = await unitOfWork.AppServiceRequestHistoryRepository.FindByAsync(e => e.Id == appservicerequesthistory.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= appservicerequesthistory.Id;
					entityUpd.ServiceRequestId= appservicerequesthistory.ServiceRequestId;
					entityUpd.ServiceRequestNo= appservicerequesthistory.ServiceRequestNo;
					entityUpd.BoardId= appservicerequesthistory.BoardId;
					entityUpd.RequestCategoryids= appservicerequesthistory.RequestCategoryids;
					entityUpd.Subject= appservicerequesthistory.Subject;
					entityUpd.Description= appservicerequesthistory.Description;
					entityUpd.Status= appservicerequesthistory.Status;
					entityUpd.Priority= appservicerequesthistory.Priority;
					entityUpd.ResolutionDate= appservicerequesthistory.ResolutionDate;
					entityUpd.UserId= appservicerequesthistory.UserId;
					entityUpd.CreatedDate= appservicerequesthistory.CreatedDate;
					entityUpd.CreatedBy= appservicerequesthistory.CreatedBy;
					entityUpd.CreatedIp= appservicerequesthistory.CreatedIp;
					entityUpd.ModifiedDate= appservicerequesthistory.ModifiedDate;
					entityUpd.ModifiedBy= appservicerequesthistory.ModifiedBy;
					entityUpd.ModifiedIp= appservicerequesthistory.ModifiedIp;
					entityUpd.InsertedOn= appservicerequesthistory.InsertedOn;
					
				await unitOfWork.AppServiceRequestHistoryRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.AppServiceRequestHistoryRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.AppServiceRequestHistoryRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppServiceRequestHistoryRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	