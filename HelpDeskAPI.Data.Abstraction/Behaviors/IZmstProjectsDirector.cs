
//-----------------------------------------------------------------------
// <copyright file="IZmstProjectsDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of ZmstProjects behavior.
    /// </summary>
    public interface IZmstProjectsDirector
    {
        /// <summary>
        ///  Get All ZmstProjects List.
        /// </summary>
        /// <returns>ZmstProjects List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstProjects>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get ZmstProjects Entity.
        /// </summary>
        /// <returns>ZmstProjects Entity.</returns>
        /// <param name="AgencyId">AgencyId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ZmstProjects> GetByIdAsync(int AgencyId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get All ZmstProjects List.
        /// </summary>
        /// <returns>ZmstProjects List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ZmstProjects>> GetByUserIdAsync(string userId,CancellationToken cancellationToken);
        

        /// <summary>
        /// Delete ZmstProjects.
        /// </summary>
        /// <param name="AgencyId">AgencyId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int AgencyId, CancellationToken cancellationToken);

        /// <summary>
        /// Save ZmstProjects.
        /// </summary>
        /// <param name="zmstprojects">zmstprojects Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(ZmstProjects zmstprojects, CancellationToken cancellationToken);

        /// <summary>
        /// Update ZmstProjects.
        /// </summary>
        /// <param name="zmstprojects">zmstprojects Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(ZmstProjects zmstprojects, CancellationToken cancellationToken);
		
    }
}
