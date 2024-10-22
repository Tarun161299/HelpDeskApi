using HelpDeskAPI.Data.Abstractions.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    public interface IJwtAuthenticationDirector
    {
        /// <summary>
        ///  Get All MdActionType List.
        /// </summary>
        /// <returns>MdActionType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Token> Authenticate(string username,string Role,string mode,CancellationToken cancellationToken);

        Task<Abstractions.Models.UserAuthorization> RefreshToken(string token, string refreshToken,string userName,string role, string mode, CancellationToken cancellationToken);
    }
}
