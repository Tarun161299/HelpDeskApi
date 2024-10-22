
//-----------------------------------------------------------------------
// <copyright file="MdActionTypeDirector.cs" company="NIC">
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
    public class MdActionTypeDirector : IMdActionTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdActionTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdActionTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdActionType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdactiontypelist = await this.unitOfWork.MdActionTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdActionType>>(mdactiontypelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdActionType> GetByIdAsync(int ActionTypeId, CancellationToken cancellationToken)
        {
            var mdactiontypelist = await this.unitOfWork.MdActionTypeRepository.FindByAsync(x => x.ActionTypeId == ActionTypeId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdActionType>(mdactiontypelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdActionType mdactiontype, CancellationToken cancellationToken)
        {
            if (mdactiontype == null)
            {
                throw new ArgumentNullException(nameof(mdactiontype));
            }

            var chkefmdactiontype = await this.unitOfWork.MdActionTypeRepository.FindByAsync(r => r.ActionTypeId == mdactiontype.ActionTypeId, default);
            if (chkefmdactiontype != null)
            {
                throw new EntityFoundException($"This Records {chkefmdactiontype} already exists");
            }

            var efmdactiontype = this.mapper.Map<Data.EF.Models.MdActionType>(mdactiontype);

            await this.unitOfWork.MdActionTypeRepository.InsertAsync(efmdactiontype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdActionType mdactiontype, CancellationToken cancellationToken)
        
		{
            if (mdactiontype.ActionTypeId == 0)
            {
                throw new ArgumentException(nameof(mdactiontype.ActionTypeId));
            }
			
			Data.EF.Models.MdActionType entityUpd = await unitOfWork.MdActionTypeRepository.FindByAsync(e => e.ActionTypeId == mdactiontype.ActionTypeId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.ActionTypeId= mdactiontype.ActionTypeId;
					entityUpd.ActionType= mdactiontype.ActionType;
					entityUpd.IsActive= mdactiontype.IsActive;
					
				await unitOfWork.MdActionTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int ActionTypeId, CancellationToken cancellationToken)
        {
            if (ActionTypeId == 0)
            {
                throw new ArgumentNullException(nameof(ActionTypeId));
            }

            var entity = await this.unitOfWork.MdActionTypeRepository.FindByAsync(x => x.ActionTypeId == ActionTypeId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ActionTypeId {ActionTypeId} was not found.");
            }

            await this.unitOfWork.MdActionTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdActionTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	