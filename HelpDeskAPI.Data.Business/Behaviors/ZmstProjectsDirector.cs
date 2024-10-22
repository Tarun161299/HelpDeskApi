
//-----------------------------------------------------------------------
// <copyright file="ZmstProjectsDirector.cs" company="NIC">
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
    using System.Text;
    using HelpDeskAPI.Data.Business.Services;

    /// <inheritdoc />
    public class ZmstProjectsDirector : IZmstProjectsDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly EncryptionDecryptionService decryptionService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstProjectsDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public ZmstProjectsDirector(IMapper mapper, IUnitOfWork unitOfWork, EncryptionDecryptionService _encryptionDecryptionService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.decryptionService = _encryptionDecryptionService;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.ZmstProjects>> GetByUserIdAsync(string userId,CancellationToken cancellationToken)
        {
            string decryptedUserId = decryptionService.Decryption(userId);
            //string decryptedUserId = Encoding.UTF8.GetString(Convert.FromBase64String(userId));
            var zmstprojectslist = await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var mdUserBoardRoleMappingList = await this.unitOfWork.MdUserBoardRoleMappingRepository .GetAllAsync(cancellationToken).ConfigureAwait(false);

            var result = from mdUserBoardRoleMapping in mdUserBoardRoleMappingList
                         join zmstprojects in zmstprojectslist on mdUserBoardRoleMapping.BoardId equals zmstprojects.ProjectId
                         where (mdUserBoardRoleMapping.UserId).Trim() == decryptedUserId
                         select new AbsModels.ZmstProjects
                         {
                             ProjectId = zmstprojects.ProjectId,
                             ProjectName = zmstprojects.ProjectName,
                         };

            return this.mapper.Map<List<AbsModels.ZmstProjects>>(result).DistinctBy(x=>x.ProjectId).ToList();
        }

        public virtual async Task<List<AbsModels.ZmstProjects>> GetAllAsync(CancellationToken cancellationToken)
        {
            var zmstprojectslist = await this.unitOfWork.ZmstProjectsRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

            return this.mapper.Map<List<AbsModels.ZmstProjects>>(zmstprojectslist);
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.ZmstProjects> GetByIdAsync(int AgencyId, CancellationToken cancellationToken)
        {
            var zmstprojectslist = await this.unitOfWork.ZmstProjectsRepository.FindByAsync(x => x.AgencyId == AgencyId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.ZmstProjects>(zmstprojectslist);
            return result;
        }

		/// <inheritdoc/>
        public async Task<int> InsertAsync(AbsModels.ZmstProjects zmstprojects, CancellationToken cancellationToken)
        {
            if (zmstprojects == null)
            {
                throw new ArgumentNullException(nameof(zmstprojects));
            }

            var chkefzmstprojects = await this.unitOfWork.ZmstProjectsRepository.FindByAsync(r => r.AgencyId == zmstprojects.AgencyId, default);
            if (chkefzmstprojects != null)
            {
                throw new EntityFoundException($"This Records {chkefzmstprojects} already exists");
            }

            var efzmstprojects = this.mapper.Map<Data.EF.Models.ZmstProjects>(zmstprojects);

            await this.unitOfWork.ZmstProjectsRepository.InsertAsync(efzmstprojects, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
        }
		
		/// <inheritdoc/>
		
        public virtual async Task<int> UpdateAsync(AbsModels.ZmstProjects zmstprojects, CancellationToken cancellationToken)
        
		{
            if (zmstprojects.AgencyId == 0)
            {
                throw new ArgumentException(nameof(zmstprojects.AgencyId));
            }
			
			Data.EF.Models.ZmstProjects entityUpd = await unitOfWork.ZmstProjectsRepository.FindByAsync(e => e.AgencyId == zmstprojects.AgencyId, cancellationToken);
			if (entityUpd != null)
            {
			entityUpd.AgencyId= zmstprojects.AgencyId;
					entityUpd.ExamCounsid= zmstprojects.ExamCounsid;
					entityUpd.AcademicYear= zmstprojects.AcademicYear;
					entityUpd.ServiceType= zmstprojects.ServiceType;
					entityUpd.Attempt= zmstprojects.Attempt;
					entityUpd.ProjectId= zmstprojects.ProjectId;
					entityUpd.ProjectName= zmstprojects.ProjectName;
					entityUpd.Description= zmstprojects.Description;
					////entityUpd.RequestLetter= zmstprojects.RequestLetter;
					entityUpd.CreatedDate= zmstprojects.CreatedDate;
					entityUpd.CreatedBy= zmstprojects.CreatedBy;
					entityUpd.ModifiedDate= zmstprojects.ModifiedDate;
					entityUpd.ModifiedBy= zmstprojects.ModifiedBy;
					entityUpd.IsLive= zmstprojects.IsLive;
					entityUpd.Pinitiated= zmstprojects.PInitiated;
					
				await unitOfWork.ZmstProjectsRepository.UpdateAsync(entityUpd, cancellationToken).ConfigureAwait(false);
				                
            }

            return await unitOfWork.CommitAsync(cancellationToken);
        }
		
		/// <inheritdoc/>
        public async Task<int> DeleteAsync(int AgencyId, CancellationToken cancellationToken)
        {
            if (AgencyId == 0)
            {
                throw new ArgumentNullException(nameof(AgencyId));
            }

            var entity = await this.unitOfWork.ZmstProjectsRepository.FindByAsync(x => x.AgencyId == AgencyId, cancellationToken).ConfigureAwait(false);

            if (entity == null)
            {
                throw new EntityNotFoundException($"The Data with an AgencyId {AgencyId} was not found.");
            }

            await this.unitOfWork.ZmstProjectsRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.ZmstProjectsRepository.CommitAsync(cancellationToken).ConfigureAwait(false);
		}
	}
	}
	