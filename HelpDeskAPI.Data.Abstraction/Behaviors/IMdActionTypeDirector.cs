
//-----------------------------------------------------------------------
// <copyright file="IMdActionTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdActionType behavior.
    /// </summary>
    public interface IMdActionTypeDirector
    {
        /// <summary>
        ///  Get All MdActionType List.
        /// </summary>
        /// <returns>MdActionType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdActionType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdActionType Entity.
        /// </summary>
        /// <returns>MdActionType Entity.</returns>
        /// <param name="ActionTypeId">ActionTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdActionType> GetByIdAsync(int ActionTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdActionType.
        /// </summary>
        /// <param name="ActionTypeId">ActionTypeId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int ActionTypeId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdActionType.
        /// </summary>
        /// <param name="mdactiontype">mdactiontype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdActionType mdactiontype, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdActionType.
        /// </summary>
        /// <param name="mdactiontype">mdactiontype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdActionType mdactiontype, CancellationToken cancellationToken);
		
    }
}
