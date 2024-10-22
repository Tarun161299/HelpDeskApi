
//-----------------------------------------------------------------------
// <copyright file="IMdSectionDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdSection behavior.
    /// </summary>
    public interface IMdSectionDirector
    {
        /// <summary>
        ///  Get All MdSection List.
        /// </summary>
        /// <returns>MdSection List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdSection>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdSection Entity.
        /// </summary>
        /// <returns>MdSection Entity.</returns>
        /// <param name="SectionId">SectionId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdSection> GetByIdAsync(int SectionId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdSection.
        /// </summary>
        /// <param name="SectionId">SectionId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int SectionId, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdSection.
        /// </summary>
        /// <param name="mdsection">mdsection Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdSection mdsection, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdSection.
        /// </summary>
        /// <param name="mdsection">mdsection Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdSection mdsection, CancellationToken cancellationToken);
		
    }
}
