
//-----------------------------------------------------------------------
// <copyright file="IAppRemarksDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppRemarks behavior.
    /// </summary>
    public interface IAppRemarksDirector
    {
        /// <summary>
        ///  Get All AppRemarks List.
        /// </summary>
        /// <returns>AppRemarks List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppRemarks>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppRemarks Entity.
        /// </summary>
        /// <returns>AppRemarks Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppRemarks> GetByIdAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppRemarks Entity.
        /// </summary>
        /// <returns>AppRemarks Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppRemarks>> GetByModuleIdAsync(AppRemarksData appRemarksData, CancellationToken cancellationToken);



        /// <summary>
        /// Delete AppRemarks.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppRemarks.
        /// </summary>
        /// <param name="appremarks">appremarks Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppRemarks appremarks, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppRemarks.
        /// </summary>
        /// <param name="appremarks">appremarks Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppRemarks appremarks, CancellationToken cancellationToken);
		
    }
}
