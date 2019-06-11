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
        /// Subscribes a the given user to the event in the DTO
        /// </summary>
        /// <param name="subscriptionRequest">The DTO containing information about de subscription</param>
        /// <param name="userId">the user subscribing to de event</param>
        /// <returns>Subscription information</returns>
        Task<SubscribeToEventResponse> SubscribeAsync(SubscribeToEventRequest subscriptionRequest, long userId);
    }
}
