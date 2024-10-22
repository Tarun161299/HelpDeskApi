
//-----------------------------------------------------------------------
// <copyright file="IAppServiceRequestHistoryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppServiceRequestHistory behavior.
    /// </summary>
    public interface IAppServiceRequestHistoryDirector
    {
        /// <summary>
        ///  Get All AppServiceRequestHistory List.
        /// </summary>
        /// <returns>AppServiceRequestHistory List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppServiceRequestHistory>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppServiceRequestHistory Entity.
        /// </summary>
        /// <returns>AppServiceRequestHistory Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppServiceRequestHistory> GetByIdAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppServiceRequestHistory.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppServiceRequestHistory.
        /// </summary>
        /// <param name="appservicerequesthistory">appservicerequesthistory Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppServiceRequestHistory appservicerequesthistory, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppServiceRequestHistory.
        /// </summary>
        /// <param name="appservicerequesthistory">appservicerequesthistory Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppServiceRequestHistory appservicerequesthistory, CancellationToken cancellationToken);
		
    }
}
