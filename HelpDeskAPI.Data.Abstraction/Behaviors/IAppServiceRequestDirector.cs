
//-----------------------------------------------------------------------
// <copyright file="IAppServiceRequestDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;


    /// <summary>
    /// Director of AppServiceRequest behavior.
    /// </summary>
    public interface IAppServiceRequestDirector
    {
        /// <summary>
        ///  Get All AppServiceRequest List.
        /// </summary>
        /// <returns>AppServiceRequest List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppServiceRequestsList>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get AppServiceRequest Entity.
        /// </summary>
        /// <returns>AppServiceRequest Entity.</returns>
        /// <param name="ServiceRequestId">ServiceRequestId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppServiceRequestsList>> GetByIdAsync(GetServiceByUserAndStatus Userdetails, CancellationToken cancellationToken);
        
        /// <summary>
        ///  Get AppServiceRequest Entity.
        /// </summary>
        /// <returns>AppServiceRequest Entity.</returns>
        /// <param name="ServiceRequestId">ServiceRequestId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppServiceRequestsList>> GetByRequestIdAsync(string ServiceRequestId, CancellationToken cancellationToken);
        /// <summary>
        /// Delete AppServiceRequest.
        /// </summary>
        /// <param name="ServiceRequestId">ServiceRequestId Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> DeleteAsync(int ServiceRequestId, CancellationToken cancellationToken);

        /// <summary>
        /// Save AppServiceRequest.
        /// </summary>
        /// <param name="appservicerequest">appservicerequest Parameter.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> InsertAsync(AppServiceRequest appservicerequest, CancellationToken cancellationToken);

        /// <summary>
        /// Update AppServiceRequest.
        /// </summary>
        /// <param name="appservicerequest">appservicerequest Parameter.</param> 
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> UpdateAsync(AppServiceRequest appServiceRequest, CancellationToken cancellationToken);
       
        /// <summary>
        ///  Get AppServiceRequest Entity.
        /// </summary>
        /// <returns>AppServiceRequest Entity.</returns>
        /// <param name="ServiceRequestId">ServiceRequestId Parameter.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<AppServiceRequestsList>> GetByIdAsync(getserviceRequest appServiceRequest, CancellationToken cancellationToken);

        //Task<int> InsertAsync(AppServiceRequest appServiceRequest, CancellationToken cancellationToken);

        /// <summary>
        ///  Get Dsashboard Status Count.
        /// </summary>
        /// <returns>Onboarding AppOnboardingRequest data by Id.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
       // Task<DashboardCount> GetStatusCountAsync(int BoardId,CancellationToken cancellationToken);
        Task<DashboardCount> GetStatusCountAsync(StatusCount statusCount, CancellationToken cancellationToken);

        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="OTP">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> SendOTP(OTPModal otpModal, CancellationToken cancellationToken);

        /// <summary>
        ///  send OTP.
        /// </summary>
        /// <returns>AppOnboardingRequest.</returns>
        /// <param name="email">onboard.</param>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<string> GetIPAddress(CancellationToken cancellationToken);

    }
}
