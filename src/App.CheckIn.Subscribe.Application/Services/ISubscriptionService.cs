using System.Threading.Tasks;
using AppCheckInSubscribe.Application.DTOs;

namespace AppCheckInSubscribe.Application
{
    /// <summary>
    /// Defines operation related to Subscriptions
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Checks is the user is subscribed to a given event
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="eventCode">the event code</param>
        /// <returns>A boolean indicating whether or not the user is subscribed to the event</returns>
        Task<bool> IsSubscribedAsync(long userId, string eventCode);

        /// <summary>
        /// Subscribes a the given user to the event in the DTO
        /// </summary>
        /// <param name="subscriptionRequest">The DTO containing information about de subscription</param>
        /// <param name="userId">the user subscribing to de event</param>
        /// <returns>Subscription information</returns>
        Task<SubscribeToEventResponse> SubscribeAsync(SubscribeToEventRequest subscriptionRequest, long userId);

        /// <summary>
        /// Deletes the user subscriptions from the given event
        /// </summary>
        /// <param name="userId">The user</param>
        /// <param name="eventCode">The event code</param>
        Task DeleteSubscriptionAsync(long userId, string eventCode);
    }
}
