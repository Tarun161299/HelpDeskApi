
//-----------------------------------------------------------------------
// <copyright file="IExtEndPointDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;


    /// <summary>
    /// Director of AppServiceRequest behavior.
    /// </summary>
    public interface IExtEndPointDirector
    {

        /// <summary>
        /// Save ESSOData.
        /// </summary>
        /// <param name="appservicerequest">appservicerequest Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(string access_token, string expires_in, string refresh_token, string token_type, string token_id, string claim_data, CancellationToken cancellationToken);

        /// <summary>
        ///  Get ESSOData Entity.
        /// </summary>
        /// <returns>AppServiceRequest Entity.</returns>
        /// <param name="id">ServiceRequestId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Esodata> GetByIdAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ESSOData.
        /// </summary>
        /// <param name="id">RoleId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        //Task<int> DeleteAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Logout ESSOData.
        /// </summary>
        /// <param name="logout">logout Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> LogoutAsync(Logout logout, CancellationToken cancellationToken);


    }
}
