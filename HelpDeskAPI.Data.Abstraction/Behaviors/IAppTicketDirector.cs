
//-----------------------------------------------------------------------
// <copyright file="IAppTicketDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppTicket behavior.
    /// </summary>
    public interface IAppTicketDirector
    {
        /// <summary>
        ///  Get All AppTicket List.
        /// </summary>
        /// <returns>AppTicket List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<GetTicketListByID>> GetAllAsync(GetTicketByUserAndStatus userIdAndStatus, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppTicket Entity.
        /// </summary>
        /// <returns>AppTicket Entity.</returns>
        /// <param name="TicketId">TicketId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<GetTicketListByID>> GetByIdAsync(int TicketId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppTicket.
        /// </summary>
        /// <param name="TicketId">TicketId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int TicketId, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppTicket.
        /// </summary>
        /// <param name="appticket">appticket Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<bool> InsertAsync(GenerateTicket appticket, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppTicket.
        /// </summary>
        /// <param name="appticket">appticket Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppTicket appticket, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppTicket Entity.
        /// </summary>
        /// <returns>AppTicket Entity.</returns>
        /// <param name="Boardid">TicketId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppTicket>> GetByBoardAsync(GetTicketbyService getTicketByService, CancellationToken cancellationToken);
        
    }
}
