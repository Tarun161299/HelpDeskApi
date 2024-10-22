//-----------------------------------------------------------------------
// <copyright file="MD_PriorityDirector.cs" company="NIC">
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
    public class MD_PriorityDirector:IMD_PriorityDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MdActionTypeDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MD_PriorityDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MD_priority>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var mdstatustypelist = await this.unitOfWork.MdPriorityRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                return this.mapper.Map<List<AbsModels.MD_priority>>(mdstatustypelist);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
