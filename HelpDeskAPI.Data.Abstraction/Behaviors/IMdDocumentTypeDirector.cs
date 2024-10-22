
//-----------------------------------------------------------------------
// <copyright file="IMdDocumentTypeDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of MdDocumentType behavior.
    /// </summary>
    public interface IMdDocumentTypeDirector
    {
        /// <summary>
        ///  Get All MdDocumentType List.
        /// </summary>
        /// <returns>MdDocumentType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MdDocumentType>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get MdDocumentType Entity.
        /// </summary>
        /// <returns>MdDocumentType Entity.</returns>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<MdDocumentType> GetByIdAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete MdDocumentType.
        /// </summary>
        /// <param name="Id">Id Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string Id, CancellationToken cancellationToken);

        /// <summary>
        /// Save MdDocumentType.
        /// </summary>
        /// <param name="mddocumenttype">mddocumenttype Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(MdDocumentType mddocumenttype, CancellationToken cancellationToken);

        /// <summary>
        /// Update MdDocumentType.
        /// </summary>
        /// <param name="mddocumenttype">mddocumenttype Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(MdDocumentType mddocumenttype, CancellationToken cancellationToken);
		
    }
}
