using System;
using App.CheckIn.Domain;

namespace AppCheckInSubscribe.Application.DTOs
{
    public class EventSubscriptionResponse
    {
        public EventSubscriptionResponse(EventSubscription eventSubscription)
        {
            UserEmail = eventSubscription.UserEmail;
            EventCode = eventSubscription.EventCode;
            EventName = eventSubscription.EventName;
            EventDescription = eventSubscription.EventDescription;
            EventRoom = eventSubscription.EventRoom;
            EventStartTime = eventSubscription.EventStartTime;
            EventDuration = eventSubscription.EventDuration;
            EnablePushNotification = eventSubscription.EnablePushNotification;
            Notified = eventSubscription.Notified;
            NotificationService = eventSubscription.NotificationService;
        }

        /// <summary>
        /// Email of the subscribed user
        /// </summary>
        public string UserEmail { get; private set; }

        /// <summary>
        /// Event Code
        /// </summary>
        public string EventCode { get; private set; }

        /// <summary>
        /// Name of the event
        /// </summary>
        public string EventName { get; private set; }

        /// <summary>
        /// Description of the event
        /// </summary>
        public string EventDescription { get; set; }

        /// <summary>
        /// Room of the Event
        /// </summary>
        public string EventRoom { get; private set; }

        /// <summary>
        /// Start time of the Event
        /// </summary>
        public DateTimeOffset EventStartTime { get; private set; }

        /// <summary>
        /// Duration of the event
        /// </summary>
        public TimeSpan EventDuration { get; private set; }

        /// <summary>
        /// User desires to receive notifications about this subscription
        /// </summary>
        public bool EnablePushNotification { get; private set; }

        /// <summary>
        /// Notification was already send to the user
        /// </summary>
        public bool Notified { get; private set; }

        /// <summary>
        /// Indicates which Notification Service should be used to send notifications
        /// </summary>
        public NotificationServiceType NotificationService { get; set; }
    }
}
