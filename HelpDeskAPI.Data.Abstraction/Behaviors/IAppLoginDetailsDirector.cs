
//-----------------------------------------------------------------------
// <copyright file="IAppLoginDetailsDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;

    /// <summary>
    /// Director of AppLoginDetails behavior.
    /// </summary>
    public interface IAppLoginDetailsDirector
    {
        /// <summary>
        ///  Get All AppLoginDetails List.
        /// </summary>
        /// <returns>AppLoginDetails List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppLoginDetails>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppLoginDetails Entity.
        /// </summary>
        /// <returns>AppLoginDetails Entity.</returns>
        /// <param name="UserId">UserId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppLoginDetails> GetByIdAsync(string UserId, CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppLoginDetails Entity.
        /// </summary>
        /// <returns>AppLoginDetails Entity.</returns>
        /// <param name="UserId">UserId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<AppLoginDetails> GetByEisIdAsync(string eisId, CancellationToken cancellationToken);

        /// <summary>
        /// Delete AppLoginDetails.
        /// </summary>
        /// <param name="UserId">UserId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(string UserId, CancellationToken cancellationToken);
        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> GetCaptcha(CancellationToken cancellationToken);
        /// <summary>
        /// Save AppLoginDetails.
        /// </summary>
        /// <param name="applogindetails">applogindetails Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppLoginDetails applogindetails, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppLoginDetails.
        /// </summary>
        /// <param name="applogindetails">applogindetails Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<int> UpdateAsync(AppLoginDetails applogindetails, CancellationToken cancellationToken);

        /// <summary>
        ///  Check userID Availibilty.
        /// </summary>
        /// <returns>userID.</returns>
        /// <param name="userID">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<bool> CheckUserIdAvailibity(string userID, CancellationToken cancellationToken);

        /// <summary>
        ///  Insert SignUp Data.
        /// </summary>
        /// <returns>SignUp.</returns>
        /// <param name="signUpData">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> SaveSignUpDetailsAsync(SignUp signUpData, CancellationToken cancellationToken);

    }
}
