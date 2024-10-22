
//-----------------------------------------------------------------------
// <copyright file="MdUserBoardRoleMappingDirector.cs" company="NIC">
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
    public class MdUserBoardRoleMappingDirector : IMdUserBoardRoleMappingDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdUserBoardRoleMappingDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdUserBoardRoleMappingDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdUserBoardRoleMapping>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mduserboardrolemappinglist = await this.unitOfWork.MdUserBoardRoleMappingRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdUserBoardRoleMapping>>(mduserboardrolemappinglist);
        }

		/// <inheritdoc/>
        public virtual async Task<List<AbsModels.MdUserBoardRoleMapping>> GetByIdAsync(string board, CancellationToken cancellationToken)
        {
            try
            {
                var mduserboardrolemappinglist = await this.unitOfWork.MdUserBoardRoleMappingRepository.FindAllByAsync(x => x.UserId == board, cancellationToken).ConfigureAwait(false);
                var result = this.mapper.Map<List<Abstractions.Models.MdUserBoardRoleMapping>>(mduserboardrolemappinglist);
                return result.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public virtual async Task<List<AbsModels.MdUserBoardRoleMapping>> GetByBoardIdAsync(string board, CancellationToken cancellationToken)
        {
            try
            {
                var mduserboardrolemappinglist = await this.unitOfWork.MdUserBoardRoleMappingRepository.FindAllByAsync(x => x.BoardId == Convert.ToInt64(board), cancellationToken).ConfigureAwait(false);
                var result = this.mapper.Map<List<Abstractions.Models.MdUserBoardRoleMapping>>(mduserboardrolemappinglist);
                return result.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdUserBoardRoleMapping mduserboardrolemapping, CancellationToken cancellationToken)
        {
            if (mduserboardrolemapping == null)
            {
                throw new ArgumentNullException(nameof(mduserboardrolemapping));
            }

            var chkefmduserboardrolemapping = await this.unitOfWork.MdUserBoardRoleMappingRepository.FindByAsync(r => r.UserId == mduserboardrolemapping.UserId, default);
            if (chkefmduserboardrolemapping != null)
            {
                throw new EntityFoundException($"This Records {chkefmduserboardrolemapping} already exists");
            }

            var efmduserboardrolemapping = this.mapper.Map<Data.EF.Models.MdUserBoardRoleMapping>(mduserboardrolemapping);

            await this.unitOfWork.MdUserBoardRoleMappingRepository.InsertAsync(efmduserboardrolemapping, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdUserBoardRoleMapping mduserboardrolemapping, CancellationToken cancellationToken)
        
		{
            if (mduserboardrolemapping.UserId == "0")
            {
                throw new ArgumentException(nameof(mduserboardrolemapping.UserId));
            }
			
			Data.EF.Models.MdUserBoardRoleMapping entityUpd = await unitOfWork.MdUserBoardRoleMappingRepository.FindByAsync(e => e.UserId == mduserboardrolemapping.UserId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.UserId= mduserboardrolemapping.UserId;
					entityUpd.BoardId= mduserboardrolemapping.BoardId;
					entityUpd.RoleId= mduserboardrolemapping.RoleId;
					entityUpd.IsActive= mduserboardrolemapping.IsActive;
					
				await unitOfWork.MdUserBoardRoleMappingRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
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

            var entity = await this.unitOfWork.MdUserBoardRoleMappingRepository.FindByAsync(x => x.UserId == UserId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an UserId {UserId} was not found.");
            }

            await this.unitOfWork.MdUserBoardRoleMappingRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdUserBoardRoleMappingRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	