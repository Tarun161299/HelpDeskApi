using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDeskAPI.Data.Abstractions.Models;
namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    public interface IMdAgencyDirector
    {
        /// <summary>
        ///  Get All MdAgency List.
        /// </summary>
        /// <returns>MdAgency List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdAgency>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdAgency Entity.
        /// </summary>
        /// <returns>MdAgency Entity.</returns>
        /// <param name="AgencyId">AgencyId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdAgency> GetByIdAsync(int AgencyId, CancellationToken cancellationToken);
    }
}
