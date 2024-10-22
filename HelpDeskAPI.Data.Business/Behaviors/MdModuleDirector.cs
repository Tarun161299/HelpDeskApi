
//-----------------------------------------------------------------------
// <copyright file="MdModuleDirector.cs" company="NIC">
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
    public class MdModuleDirector : IMdModuleDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdModuleDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdModuleDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdModule>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdmodulelist = await this.unitOfWork.MdModuleRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdModule>>(mdmodulelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdModule> GetByIdAsync(string ModuleId, CancellationToken cancellationToken)
        {
            var mdmodulelist = await this.unitOfWork.MdModuleRepository.FindByAsync(x => x.ModuleId == ModuleId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdModule>(mdmodulelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdModule mdmodule, CancellationToken cancellationToken)
        {
            if (mdmodule == null)
            {
                throw new ArgumentNullException(nameof(mdmodule));
            }

            var chkefmdmodule = await this.unitOfWork.MdModuleRepository.FindByAsync(r => r.ModuleId == mdmodule.ModuleId, default);
            if (chkefmdmodule != null)
            {
                throw new EntityFoundException($"This Records {chkefmdmodule} already exists");
            }

            var efmdmodule = this.mapper.Map<Data.EF.Models.MdModule>(mdmodule);

            await this.unitOfWork.MdModuleRepository.InsertAsync(efmdmodule, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdModule mdmodule, CancellationToken cancellationToken)
        
		{
            if (mdmodule.ModuleId == "0")
            {
                throw new ArgumentException(nameof(mdmodule.ModuleId));
            }
			
			Data.EF.Models.MdModule entityUpd = await unitOfWork.MdModuleRepository.FindByAsync(e => e.ModuleId == mdmodule.ModuleId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.ModuleId= mdmodule.ModuleId;
					entityUpd.Heading= mdmodule.Heading;
					entityUpd.SubHeading= mdmodule.SubHeading;
					entityUpd.Url= mdmodule.Url;
					entityUpd.Path= mdmodule.Path;
					entityUpd.Parent= mdmodule.Parent;
					entityUpd.Description= mdmodule.Description;
					entityUpd.IsActive= mdmodule.IsActive;
					
				await unitOfWork.MdModuleRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string ModuleId, CancellationToken cancellationToken)
        {
            if (ModuleId == "0")
            {
                throw new ArgumentNullException(nameof(ModuleId));
            }

            var entity = await this.unitOfWork.MdModuleRepository.FindByAsync(x => x.ModuleId == ModuleId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an ModuleId {ModuleId} was not found.");
            }

            await this.unitOfWork.MdModuleRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdModuleRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	