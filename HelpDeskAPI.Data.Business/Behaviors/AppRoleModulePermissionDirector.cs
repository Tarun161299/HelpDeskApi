
//-----------------------------------------------------------------------
// <copyright file="AppRoleModulePermissionDirector.cs" company="NIC">
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
    public class AppRoleModulePermissionDirector : IAppRoleModulePermissionDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppRoleModulePermissionDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppRoleModulePermissionDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.AppRoleModulePermission>> GetAllAsync(CancellationToken cancellationToken)
        {
            var approlemodulepermissionlist = await this.unitOfWork.AppRoleModulePermissionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.AppRoleModulePermission>>(approlemodulepermissionlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.AppRoleModulePermission> GetByIdAsync(int RoleId, CancellationToken cancellationToken)
        {
            var approlemodulepermissionlist = await this.unitOfWork.AppRoleModulePermissionRepository.FindByAsync(x => x.RoleId == RoleId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppRoleModulePermission>(approlemodulepermissionlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.AppRoleModulePermission approlemodulepermission, CancellationToken cancellationToken)
        {
            if (approlemodulepermission == null)
            {
                throw new ArgumentNullException(nameof(approlemodulepermission));
            }

            var chkefapprolemodulepermission = await this.unitOfWork.AppRoleModulePermissionRepository.FindByAsync(r => r.RoleId == approlemodulepermission.RoleId, default);
            if (chkefapprolemodulepermission != null)
            {
                throw new EntityFoundException($"This Records {chkefapprolemodulepermission} already exists");
            }

            var efapprolemodulepermission = this.mapper.Map<Data.EF.Models.AppRoleModulePermission>(approlemodulepermission);

            await this.unitOfWork.AppRoleModulePermissionRepository.InsertAsync(efapprolemodulepermission, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.AppRoleModulePermission approlemodulepermission, CancellationToken cancellationToken)
        
		{
            if (approlemodulepermission.RoleId == 0)
            {
                throw new ArgumentException(nameof(approlemodulepermission.RoleId));
            }
			
			Data.EF.Models.AppRoleModulePermission entityUpd = await unitOfWork.AppRoleModulePermissionRepository.FindByAsync(e => e.RoleId == approlemodulepermission.RoleId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.RoleId= approlemodulepermission.RoleId;
					entityUpd.ModuleId= approlemodulepermission.ModuleId;
					entityUpd.IsActive= approlemodulepermission.IsActive;
					
				await unitOfWork.AppRoleModulePermissionRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int RoleId, CancellationToken cancellationToken)
        {
            if (RoleId == 0)
            {
                throw new ArgumentNullException(nameof(RoleId));
            }

            var entity = await this.unitOfWork.AppRoleModulePermissionRepository.FindByAsync(x => x.RoleId == RoleId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an RoleId {RoleId} was not found.");
            }

            await this.unitOfWork.AppRoleModulePermissionRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AppRoleModulePermissionRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	