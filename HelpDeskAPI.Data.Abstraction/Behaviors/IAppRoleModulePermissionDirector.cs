
//-----------------------------------------------------------------------
// <copyright file="IAppRoleModulePermissionDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppRoleModulePermission behavior.
    /// </summary>
    public interface IAppRoleModulePermissionDirector
    {
        /// <summary>
        ///  Get All AppRoleModulePermission List.
        /// </summary>
        /// <returns>AppRoleModulePermission List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppRoleModulePermission>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppRoleModulePermission Entity.
        /// </summary>
        /// <returns>AppRoleModulePermission Entity.</returns>
        /// <param name="RoleId">RoleId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppRoleModulePermission> GetByIdAsync(int RoleId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppRoleModulePermission.
        /// </summary>
        /// <param name="RoleId">RoleId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int RoleId, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppRoleModulePermission.
        /// </summary>
        /// <param name="approlemodulepermission">approlemodulepermission Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppRoleModulePermission approlemodulepermission, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppRoleModulePermission.
        /// </summary>
        /// <param name="approlemodulepermission">approlemodulepermission Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppRoleModulePermission approlemodulepermission, CancellationToken cancellationToken);
		
    }
}
