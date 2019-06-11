using System;

namespace AppCheckInSubscribe.Application.DTOs
{
    public class SubscribeToEventRequest
    {
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
        /// Start time of the event
        /// </summary>
        public DateTimeOffset? EventStartTime { get; set; }

        /// <summary>
        /// Duration of the event
        /// </summary>
        public int? EventDuration { get; set; }

        /// <summary>
        /// Room of the Event
        /// </summary>
        public string EventRoom { get; set; }

        /// <summary>
        /// Informs whether or not the user wants to receive notifications of the event
        /// </summary>
        public bool EnablePushNotification { get; set; } = true;

        /// <summary>
        /// Notification token required to send notification do the user device
        /// </summary>
        public string NotificationToken { get; set; }
    }
}
