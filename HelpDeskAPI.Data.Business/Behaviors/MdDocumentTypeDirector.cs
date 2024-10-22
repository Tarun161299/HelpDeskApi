
//-----------------------------------------------------------------------
// <copyright file="MdDocumentTypeDirector.cs" company="NIC">
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
    public class MdDocumentTypeDirector : IMdDocumentTypeDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdDocumentTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdDocumentTypeDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdDocumentType>> GetAllAsync(CancellationToken cancellationToken)
        {
            var mddocumenttypelist = await this.unitOfWork.MdDocumentTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            return this.mapper.Map<List<AbsModels.MdDocumentType>>(mddocumenttypelist);
        }

		/// <inheritdoc/>
        public virtual async Task<AbsModels.MdDocumentType> GetByIdAsync(string Id, CancellationToken cancellationToken)
        {
            var mddocumenttypelist = await this.unitOfWork.MdDocumentTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdDocumentType>(mddocumenttypelist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.MdDocumentType mddocumenttype, CancellationToken cancellationToken)
        {
            if (mddocumenttype == null)
            {
                throw new ArgumentNullException(nameof(mddocumenttype));
            }

            var chkefmddocumenttype = await this.unitOfWork.MdDocumentTypeRepository.FindByAsync(r => r.Id == mddocumenttype.Id, default);
            if (chkefmddocumenttype != null)
            {
                throw new EntityFoundException($"This Records {chkefmddocumenttype} already exists");
            }

            var efmddocumenttype = this.mapper.Map<Data.EF.Models.MdDocumentType>(mddocumenttype);

            await this.unitOfWork.MdDocumentTypeRepository.InsertAsync(efmddocumenttype, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.MdDocumentType mddocumenttype, CancellationToken cancellationToken)
        
		{
            if (mddocumenttype.Id == "0")
            {
                throw new ArgumentException(nameof(mddocumenttype.Id));
            }
			
			Data.EF.Models.MdDocumentType entityUpd = await unitOfWork.MdDocumentTypeRepository.FindByAsync(e => e.Id == mddocumenttype.Id, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.Id= mddocumenttype.Id;
					entityUpd.Title= mddocumenttype.Title;
					entityUpd.Format= mddocumenttype.Format;
					entityUpd.MinSize= mddocumenttype.MinSize;
					entityUpd.MaxSize= mddocumenttype.MaxSize;
					entityUpd.DisplayPriority= mddocumenttype.DisplayPriority;
					entityUpd.DocumentNatureType= mddocumenttype.DocumentNatureType;
					entityUpd.DocumentNatureTypeDesc= mddocumenttype.DocumentNatureTypeDesc;
					entityUpd.IsPasswordProtected= mddocumenttype.IsPasswordProtected;
					entityUpd.CreatedDate= mddocumenttype.CreatedDate;
					entityUpd.CreatedBy= mddocumenttype.CreatedBy;
					entityUpd.ModifiedDate= mddocumenttype.ModifiedDate;
					entityUpd.ModifiedBy= mddocumenttype.ModifiedBy;
					
				await unitOfWork.MdDocumentTypeRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(string Id, CancellationToken cancellationToken)
        {
            if (Id == "0")
            {
                throw new ArgumentNullException(nameof(Id));
            }

            var entity = await this.unitOfWork.MdDocumentTypeRepository.FindByAsync(x => x.Id == Id, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an Id {Id} was not found.");
            }

            await this.unitOfWork.MdDocumentTypeRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.MdDocumentTypeRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	