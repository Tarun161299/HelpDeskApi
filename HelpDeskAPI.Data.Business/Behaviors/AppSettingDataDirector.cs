using AutoMapper;
using HelpDeskAPI.Data.Abstractions.Behaviors;
using HelpDeskAPI.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Business.Behaviors
{
    public class AppSettingDataDirector : IAppSettingDirector
    {
        private const string Headers = "Headers:HeaderBeforeLogin";
        private const string ResolutionDate = "ResolutionDate:";
        private readonly IConfiguration _config;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceRequestHistoryDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppSettingDataDirector(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration config)
        {
            this._config=config;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> GetHeaders(CancellationToken cancellationToken)
        {//var a= _config.GetSection("Headers").Value.ToString();
            string headers= _config.GetSection(Headers).Value.ToString();
            return headers;
        }
        public async Task<int> GetResolutionDate(string priority,CancellationToken cancellationToken)
        {//var a= _config.GetSection("Headers").Value.ToString();
            string noOfDates = (_config.GetSection(ResolutionDate+priority).Value.ToString());
            return int.Parse(noOfDates);
        }

    }
}
