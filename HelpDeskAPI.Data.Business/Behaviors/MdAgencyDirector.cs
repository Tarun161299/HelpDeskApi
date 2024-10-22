using HelpDeskAPI.Data.Abstractions.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HelpDeskAPI.Data.Interfaces;
using AbsModels = HelpDeskAPI.Data.Abstractions.Models;
namespace HelpDeskAPI.Data.Business.Behaviors
{
   
    public class MdAgencyDirector : IMdAgencyDirector////jjjj
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public MdAgencyDirector(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public virtual async Task<List<AbsModels.MdAgency>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await this.unitOfWork.MdAgencyRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<List<AbsModels.MdAgency>>(list);

            return result;
        }

        /// <inheritdoc/>
        public virtual async Task<AbsModels.MdAgency> GetByIdAsync(int AgencyId, CancellationToken cancellationToken)
        {
            var mdagencylist = await this.unitOfWork.MdAgencyRepository.FindByAsync(x => x.AgencyId == AgencyId, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.MdAgency>(mdagencylist);
            return result;
        }
    }
}
