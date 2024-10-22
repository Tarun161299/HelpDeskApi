
//-----------------------------------------------------------------------
// <copyright file="IAppTicketHistoryDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppTicketHistory behavior.
    /// </summary>
    public interface IAppTicketHistoryDirector
    {
        /// <summary>
        ///  Get All AppTicketHistory List.
        /// </summary>
        /// <returns>AppTicketHistory List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppTicketHistory>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppTicketHistory Entity.
        /// </summary>
        /// <returns>AppTicketHistory Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppTicketHistory> GetByIdAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppTicketHistory.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppTicketHistory.
        /// </summary>
        /// <param name="apptickethistory">apptickethistory Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppTicketHistory apptickethistory, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppTicketHistory.
        /// </summary>
        /// <param name="apptickethistory">apptickethistory Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppTicketHistory apptickethistory, CancellationToken cancellationToken);
		
    }
}
