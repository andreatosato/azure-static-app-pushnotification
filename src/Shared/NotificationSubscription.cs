using System;

namespace Blazoring.PWA.Shared
{
    public class NotificationSubscription
    {
        public string Url { get; set; }

        public string P256dh { get; set; }

        public string Auth { get; set; }
    }

    public class NotificationMessageText
    {
        public string message { get; set; }
        public string body { get; set; }
        public NotifationAction[] actions { get; set; }
        public bool requireInteraction { get; set; } = true;
        public int[] vibrate { get; set; } = new int[] { 100, 50, 100 };
    }

    public class NotificationImage : NotificationMessageText
    {
        public string badge { get; set; }
        public string image { get; set; }
    }

    public class NotifationAction
    {
        public string action { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
    }

    /*
     *  event.waitUntil(self.registration.showNotification('Codegen 2021 news!',
        {
            body: payload.message,
            icon: 'icon-512.png',
            badge: 'icon-512.png',
            image: 'cat.png',
            vibrate: [100, 50, 100],
            data: { url: payload.url },
            requireInteraction: true,
            actions: [
                {
                    action: "agenda", title: "Open Agenda", icon: 'icon-512.png'
                },
                {
                    action: "close", title: "Ignore",
                },
            ]
        })
    );    
     */
}
