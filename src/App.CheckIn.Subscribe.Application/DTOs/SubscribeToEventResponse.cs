using System;
using App.CheckIn.Domain;

namespace AppCheckInSubscribe.Application.DTOs
{
    public class SubscribeToEventResponse
    {
        /// <summary>
        /// Identifier of the subscription
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Email of the subscribed user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Event Code
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// Name of the event
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Description of the event
        /// </summary>
        public string EventDescription { get; set; }

        /// <summary>
        /// Room of the Event
        /// </summary>
        public string EventRoom { get; set; }

        /// <summary>
        /// Start time of the event
        /// </summary>
        public DateTimeOffset? EventStartTime { get; set; }

        /// <summary>
        /// Duration of the event
        /// </summary>
        public TimeSpan? EventDuration { get; set; }

        /// <summary>
        /// User desires to receive notifications about this event
        /// </summary>
        public bool EnablePushNotification { get; set; }

        /// <summary>
        /// Indicates which Notification Service should be used to send notifications
        /// </summary>
        public NotificationServiceType NotificationService { get; set; }

        public SubscribeToEventResponse(EventSubscription subscription)
        {
            Id = subscription.Id;
            Email = subscription.UserEmail;
            EventCode = subscription.EventCode;
            EventName = subscription.EventName;
            EventDescription = subscription.EventDescription;
            EventRoom = subscription.EventRoom;
            EventStartTime = subscription.EventStartTime;
            EventDuration = subscription.EventDuration;
            EnablePushNotification = subscription.EnablePushNotification;
            NotificationService = subscription.NotificationService;
        }
    }
}
