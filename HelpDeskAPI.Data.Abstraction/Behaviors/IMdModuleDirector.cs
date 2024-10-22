
//-----------------------------------------------------------------------
// <copyright file="IMdModuleDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdModule behavior.
    /// </summary>
    public interface IMdModuleDirector
    {
        /// <summary>
        ///  Get All MdModule List.
        /// </summary>
        /// <returns>MdModule List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdModule>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdModule Entity.
        /// </summary>
        /// <returns>MdModule Entity.</returns>
        /// <param name="ModuleId">ModuleId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdModule> GetByIdAsync(string ModuleId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdModule.
        /// </summary>
        /// <param name="ModuleId">ModuleId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string ModuleId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdModule.
        /// </summary>
        /// <param name="mdmodule">mdmodule Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdModule mdmodule, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdModule.
        /// </summary>
        /// <param name="mdmodule">mdmodule Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdModule mdmodule, CancellationToken cancellationToken);
		
    }
}
