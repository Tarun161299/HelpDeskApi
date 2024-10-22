
//-----------------------------------------------------------------------
// <copyright file="MdSectionDirector.cs" company="NIC">
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
    public class MdSectionDirector : IMdSectionDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdSectionDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdSectionDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdSection>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mdsectionlist = await this.unitOfWork.MdSectionRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdSection>>(mdsectionlist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdSection> GetByIdAsync(int SectionId, CancellationToken cancellationToken)
        {
            var mdsectionlist = await this.unitOfWork.MdSectionRepository.FindByAsync(x => x.SectionId == SectionId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdSection>(mdsectionlist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdSection mdsection, CancellationToken cancellationToken)
        {
            if (mdsection == null)
            {
                throw new ArgumentNullException(nameof(mdsection));
            }

            var chkefmdsection = await this.unitOfWork.MdSectionRepository.FindByAsync(r => r.SectionId == mdsection.SectionId, default);
            if (chkefmdsection != null)
            {
                throw new EntityFoundException($"This Records {chkefmdsection} already exists");
            }

            var efmdsection = this.mapper.Map<Data.EF.Models.MdSection>(mdsection);

            await this.unitOfWork.MdSectionRepository.InsertAsync(efmdsection, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdSection mdsection, CancellationToken cancellationToken)
        
		{
            if (mdsection.SectionId == 0)
            {
                throw new ArgumentException(nameof(mdsection.SectionId));
            }
			
			Data.EF.Models.MdSection entityUpd = await unitOfWork.MdSectionRepository.FindByAsync(e => e.SectionId == mdsection.SectionId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.SectionId= mdsection.SectionId;
					entityUpd.Section= mdsection.Section;
					entityUpd.IsActive= mdsection.IsActive;
					
				await unitOfWork.MdSectionRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int SectionId, CancellationToken cancellationToken)
        {
            if (SectionId == 0)
            {
                throw new ArgumentNullException(nameof(SectionId));
            }

            var entity = await this.unitOfWork.MdSectionRepository.FindByAsync(x => x.SectionId == SectionId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an SectionId {SectionId} was not found.");
            }

            await this.unitOfWork.MdSectionRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdSectionRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	