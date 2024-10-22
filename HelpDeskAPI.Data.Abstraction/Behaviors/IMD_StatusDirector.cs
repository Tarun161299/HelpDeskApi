
//-----------------------------------------------------------------------
// <copyright file="IMdActionTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;
    public interface IMD_StatusDirector
    {
        /// <summary>
        ///  Get All MdActionType List.
        /// </summary>
        /// <returns>MdActionType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MD_Status>> GetAllAsync(CancellationToken cancellationToken);
    }
}
