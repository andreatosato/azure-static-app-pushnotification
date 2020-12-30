using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Blazoring.PWA.Shared;
using WebPush;
using Blazoring.PWA.API.Configurations;

namespace Blazoring.PWA.API
{
    public class NotificationSubscribe
    {
        private readonly WebPushNotificationConfig webPushNotification;
        private readonly ILogger<NotificationSubscribe> _logger;

        public NotificationSubscribe(IOptions<WebPushNotificationConfig> webPushNotificationOption,
           ILogger<NotificationSubscribe> logger)
        {
            this.webPushNotification = webPushNotificationOption.Value;
            _logger = logger;
        }

        [FunctionName("NotificationSubscribe")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var responseBody = await req.ReadAsStringAsync();
            NotificationSubscription subscription = JsonConvert.DeserializeObject<NotificationSubscription>(responseBody);
            var pushSubscription = new PushSubscription(subscription.Url, subscription.P256dh, subscription.Auth);
            var vapidDetails = new VapidDetails(webPushNotification.Subject, webPushNotification.PublicKey, webPushNotification.PrivateKey);
            var webPushClient = new WebPushClient();
            try
            {
                var payload = System.Text.Json.JsonSerializer.Serialize(new
                {
                    message = "Ciao",
                    url = $"myorders/10",
                });
                await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                // Save pushSubscription
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error sending push notification: " + ex.Message);
            }
            return new OkResult();
        }
    }
}
