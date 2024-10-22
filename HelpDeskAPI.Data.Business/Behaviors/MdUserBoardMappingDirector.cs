
//-----------------------------------------------------------------------
// <copyright file="MdUserBoardMappingDirector.cs" company="NIC">
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
    public class MdUserBoardMappingDirector : IMdUserBoardMappingDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdUserBoardMappingDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdUserBoardMappingDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdUserBoardMapping>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mduserboardmappinglist = await this.unitOfWork.MdUserBoardMappingRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdUserBoardMapping>>(mduserboardmappinglist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdUserBoardMapping> GetByIdAsync(string UserId, CancellationToken cancellationToken)
        {
            var mduserboardmappinglist = await this.unitOfWork.MdUserBoardMappingRepository.FindByAsync(x => x.UserId == UserId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdUserBoardMapping>(mduserboardmappinglist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdUserBoardMapping mduserboardmapping, CancellationToken cancellationToken)
        {
            if (mduserboardmapping == null)
            {
                throw new ArgumentNullException(nameof(mduserboardmapping));
            }

            var chkefmduserboardmapping = await this.unitOfWork.MdUserBoardMappingRepository.FindByAsync(r => r.UserId == mduserboardmapping.UserId, default);
            if (chkefmduserboardmapping != null)
            {
                throw new EntityFoundException($"This Records {chkefmduserboardmapping} already exists");
            }

            var efmduserboardmapping = this.mapper.Map<Data.EF.Models.MdUserBoardMapping>(mduserboardmapping);

            await this.unitOfWork.MdUserBoardMappingRepository.InsertAsync(efmduserboardmapping, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdUserBoardMapping mduserboardmapping, CancellationToken cancellationToken)
        
		{
            if (mduserboardmapping.UserId == "0")
            {
                throw new ArgumentException(nameof(mduserboardmapping.UserId));
            }
			
			Data.EF.Models.MdUserBoardMapping entityUpd = await unitOfWork.MdUserBoardMappingRepository.FindByAsync(e => e.UserId == mduserboardmapping.UserId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.UserId= mduserboardmapping.UserId;
					entityUpd.BoardId= mduserboardmapping.BoardId;
					entityUpd.IsActive= mduserboardmapping.IsActive;
					
				await unitOfWork.MdUserBoardMappingRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string UserId, CancellationToken cancellationToken)
        {
            if (UserId == "0")
            {
                throw new ArgumentNullException(nameof(UserId));
            }

            var entity = await this.unitOfWork.MdUserBoardMappingRepository.FindByAsync(x => x.UserId == UserId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an UserId {UserId} was not found.");
            }

            await this.unitOfWork.MdUserBoardMappingRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdUserBoardMappingRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	