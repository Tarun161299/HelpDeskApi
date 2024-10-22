
//-----------------------------------------------------------------------
// <copyright file="IMdUserBoardRoleMappingDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdUserBoardRoleMapping behavior.
    /// </summary>
    public interface IMdUserBoardRoleMappingDirector
    {
        /// <summary>
        ///  Get All MdUserBoardRoleMapping List.
        /// </summary>
        /// <returns>MdUserBoardRoleMapping List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdUserBoardRoleMapping>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdUserBoardRoleMapping Entity.
        /// </summary>
        /// <returns>MdUserBoardRoleMapping Entity.</returns>
        /// <param name="UserId">UserId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdUserBoardRoleMapping>> GetByIdAsync(string UserId, CancellationToken cancellationToken);
        //
        /// <summary>
        ///  Get MdUserBoardRoleMapping Entity.
        /// </summary>
        /// <returns>MdUserBoardRoleMapping Entity.</returns>
        /// <param name="UserId">UserId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdUserBoardRoleMapping>> GetByBoardIdAsync(string Boardid, CancellationToken cancellationToken);
        /// <summary>
        /// Delete MdUserBoardRoleMapping.
        /// </summary>
        /// <param name="UserId">UserId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string UserId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdUserBoardRoleMapping.
        /// </summary>
        /// <param name="mduserboardrolemapping">mduserboardrolemapping Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdUserBoardRoleMapping mduserboardrolemapping, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdUserBoardRoleMapping.
        /// </summary>
        /// <param name="mduserboardrolemapping">mduserboardrolemapping Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdUserBoardRoleMapping mduserboardrolemapping, CancellationToken cancellationToken);
		
    }
}
