using System;
using System.Threading.Tasks;
using App.CheckIn.Domain;
using App.CheckIn.Domain.Repositories;
using App.CheckIn.Domain.ValuesObjects;
using AppCheckInSubscribe.Application.DTOs;
using AppCheckInSubscribe.Application.Localization;
using Tnf.Notifications;

namespace AppCheckInSubscribe.Application
{
    /// <summary>
    /// Application service responsible for executing operations related to Subscriptions
    /// </summary>
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IEventSubscriptionRepository _subscriptionRepository;
        private readonly INotificationHandler _notification;

        public SubscriptionService(
            IEventSubscriptionRepository subscriptionRepository,
            INotificationHandler notificationHandler)
        {
            _subscriptionRepository = subscriptionRepository;
            _notification = notificationHandler;
        }

        /// <summary>
        /// Subscribes a the given user to the event in the DTO
        /// </summary>
        /// <param name="subscriptionRequest">The DTO containing information about de subscription</param>
        /// <param name="userId">the user subscribing to de event</param>
        /// <returns>Subscription information in case the subscription is successful, otherwise return null</returns>
        public async Task<SubscribeToEventResponse> SubscribeAsync(SubscribeToEventRequest subscriptionRequest, long userId)
        {
            if (!IsValidSubscription(subscriptionRequest))
            {
                return null;
            }

            var eventSubscription = await _subscriptionRepository.GetSubscriptionAsync(
                userId,
                subscriptionRequest.EventCode);

            if (eventSubscription != null)
            {
                return new SubscribeToEventResponse(eventSubscription);
            }

            eventSubscription = new EventSubscription(
                userId,
                subscriptionRequest.Email,
                subscriptionRequest.EventCode,
                subscriptionRequest.EventStartTime.Value,
                TimeSpan.FromMinutes(subscriptionRequest.EventDuration.Value),
                subscriptionRequest.EventName,
                subscriptionRequest.EventDescription,
                subscriptionRequest.EventRoom,
                subscriptionRequest.EnablePushNotification,
                subscriptionRequest.NotificationService,
                subscriptionRequest.NotificationToken);

            eventSubscription = await _subscriptionRepository.InsertAsync(eventSubscription);

            return new SubscribeToEventResponse(eventSubscription);
        }

        /// <summary>
        /// Checks is the user is subscribed to a given event
        /// </summary>
        /// <param name="userId">The user Id</param>
        /// <param name="eventCode">the event code</param>
        /// <returns>A boolean indicating whether or not the user is subscribed to the event</returns>
        public Task<bool> IsSubscribedAsync(long userId, string eventCode)
        {
            return _subscriptionRepository.HasSubscriptionAsync(userId, eventCode);
        }

        /// <summary>
        /// Deletes the user subscriptions from the given event
        /// </summary>
        /// <param name="userId">The user</param>
        /// <param name="eventCode">The event code</param>
        public async Task DeleteSubscriptionAsync(long userId, string eventCode)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionAsync(userId, eventCode);
            if (subscription != null)
            {
                await _subscriptionRepository.DeleteAsync(subscription);
            }
        }

        private bool IsValidSubscription(SubscribeToEventRequest subscription)
        {
            if (subscription.Email.IsNullOrWhiteSpace())
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.MissingEmail);
            }

            if (!subscription.Email.IsNullOrWhiteSpace() && !Email.IsValid(subscription.Email))
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.InvalidEmail, subscription.Email);
            }

            if (subscription.EventCode.IsNullOrWhiteSpace())
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.MissingEventCode);
            }

            if (subscription.EventName.IsNullOrWhiteSpace())
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.MissingEventName);
            }

            if (subscription.EventDescription.IsNullOrWhiteSpace())
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.MissingEventDescription);
            }

            if (!subscription.EventStartTime.HasValue)
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.MissingEventStartTime);
            }

            if (!subscription.EventDuration.HasValue)
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.MissingEventDuration);
            }

            if (subscription.EventRoom.IsNullOrWhiteSpace())
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.MissingEventRoom);
            }

            if (subscription.EnablePushNotification && subscription.NotificationToken.IsNullOrWhiteSpace())
            {
                _notification.RaiseError(LocalizationSources.Application, SubscriptionError.MissingNotificationToken);
            }

            var hasErrors = _notification.HasErrorNotification();

            return !hasErrors;
        }

        enum SubscriptionError
        {
            MissingEmail,
            InvalidEmail,
            MissingEventCode,
            MissingEventName,
            MissingEventDescription,
            MissingEventStartTime,
            MissingEventDuration,
            MissingEventRoom,
            MissingNotificationToken
        }
    }
}
