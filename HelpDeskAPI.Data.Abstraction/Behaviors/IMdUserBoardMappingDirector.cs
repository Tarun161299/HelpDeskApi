
//-----------------------------------------------------------------------
// <copyright file="IMdUserBoardMappingDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdUserBoardMapping behavior.
    /// </summary>
    public interface IMdUserBoardMappingDirector
    {
        /// <summary>
        ///  Get All MdUserBoardMapping List.
        /// </summary>
        /// <returns>MdUserBoardMapping List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdUserBoardMapping>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdUserBoardMapping Entity.
        /// </summary>
        /// <returns>MdUserBoardMapping Entity.</returns>
        /// <param name="UserId">UserId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdUserBoardMapping> GetByIdAsync(string UserId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdUserBoardMapping.
        /// </summary>
        /// <param name="UserId">UserId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string UserId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdUserBoardMapping.
        /// </summary>
        /// <param name="mduserboardmapping">mduserboardmapping Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdUserBoardMapping mduserboardmapping, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdUserBoardMapping.
        /// </summary>
        /// <param name="mduserboardmapping">mduserboardmapping Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdUserBoardMapping mduserboardmapping, CancellationToken cancellationToken);
		
    }
}
