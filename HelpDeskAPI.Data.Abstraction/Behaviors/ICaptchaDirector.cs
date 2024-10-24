﻿
//-----------------------------------------------------------------------
// <copyright file="ICaptchaDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;


    /// <summary>
    /// Director of AppServiceRequest behavior.
    /// </summary>
    public interface ICaptchaDirector
	{

		/// <summary>
		/// Save ESSOData.
		/// </summary>
		/// <param name="ICaptchaDirector">ICaptchaDirector Parameter.</param>
		/// <param name="cancellationToken">cancellationToken.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		Task<AppCaptcha> InsertAsync(string key,string base64String,string hashvalue, CancellationToken cancellationToken);

		/// <summary>
		///  Get ESSOData Entity.
		/// </summary>
		/// <returns>ICaptchaDirector Entity.</returns>
		/// <param name="id">ServiceRequestId Parameter.</param>
		/// <param name="cancellationToken">cancellation Token.</param>
		/// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
		Task<Esodata> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete ESSOData.
        /// </summary>
        /// <param name="id">RoleId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        //Task<int> DeleteAsync(int id, CancellationToken cancellationToken);

        
        Task<int> checkCaptcha(Check_captcha captcha, CancellationToken cancellationToken);

    }
}
