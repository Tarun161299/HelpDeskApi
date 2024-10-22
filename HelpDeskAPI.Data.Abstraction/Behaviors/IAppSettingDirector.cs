using HelpDeskAPI.Data.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    public interface IAppSettingDirector
    {
        /// <summary>
        ///  Get All AppServiceRequestHistory List.
        /// </summary>
        /// <returns>AppServiceRequestHistory List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> GetHeaders(CancellationToken cancellationToken);

        /// <summary>
        ///  Get All AppServiceRequestHistory List.
        /// </summary>
        /// <returns>AppServiceRequestHistory List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> GetResolutionDate(string priority,CancellationToken cancellationToken);
    }
}
