

namespace HelpDeskAPI.Data.Abstractions.Behaviors
{
    using HelpDeskAPI.Data.Abstractions.Models;
    public interface IMD_PriorityDirector
    { 
        /// <summary>
        ///  Get All MdActionType List.
        /// </summary>
        /// <returns>MdActionType List.</returns>
        /// <param name="cancellationToken">cancellation Token.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<MD_priority>> GetAllAsync(CancellationToken cancellationToken);
    }
}
