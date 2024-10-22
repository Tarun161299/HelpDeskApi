
//-----------------------------------------------------------------------
// <copyright file="IAppDocumentUploadedDetailDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppDocumentUploadedDetail behavior.
    /// </summary>
    public interface IAppDocumentUploadedDetailDirector
    {
        /// <summary>
        ///  Get All AppDocumentUploadedDetail List.
        /// </summary>
        /// <returns>AppDocumentUploadedDetail List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppDocumentUploadedDetail>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppDocumentUploadedDetail Entity.
        /// </summary>
        /// <returns>AppDocumentUploadedDetail Entity.</returns>
        /// <param name="DocumentId">DocumentId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppDocumentUploadedDetail> GetByIdAsync(int fileId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppDocumentUploadedDetail.
        /// </summary>
        /// <param name="DocumentId">DocumentId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int DocumentId, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppDocumentUploadedDetail.
        /// </summary>
        /// <param name="appdocumentuploadeddetail">appdocumentuploadeddetail Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(UpdateDocuments appdocumentuploadeddetail, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppDocumentUploadedDetail.
        /// </summary>
        /// <param name="appdocumentuploadeddetail">appdocumentuploadeddetail Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppDocumentUploadedDetail appdocumentuploadeddetail, CancellationToken cancellationToken);
		
    }
}
