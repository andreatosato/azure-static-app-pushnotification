using System;

namespace Blazoring.PWA.Shared
{
    public class NotificationSubscription
    {
        public int NotificationSubscriptionId { get; set; }

        public string Username { get; set; }

        public string Url { get; set; }

        public string P256dh { get; set; }

        public string Auth { get; set; }
    }
}
