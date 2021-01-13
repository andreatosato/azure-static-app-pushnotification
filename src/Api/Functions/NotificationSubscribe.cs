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
using Blazoring.PWA.API.Services;

namespace Blazoring.PWA.API
{
    public class NotificationSubscribe
    {
        private readonly WebPushNotificationConfig webPushNotification;
        private readonly ILogger<NotificationSubscribe> _logger;
        private IStorageService _storageService;

        public NotificationSubscribe(IOptions<WebPushNotificationConfig> webPushNotificationOption,
           IStorageService storageService,
           ILogger<NotificationSubscribe> logger)
        {
            this.webPushNotification = webPushNotificationOption.Value;
            _logger = logger;
            _storageService = storageService;
        }

        [FunctionName("NotificationSubscribe")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var responseBody = await req.ReadAsStringAsync();
            NotificationSubscription subscription = JsonConvert.DeserializeObject<NotificationSubscription>(responseBody);
            await _storageService.AddPushNotification(subscription);

            //var vapidDetails = new VapidDetails(webPushNotification.Subject, webPushNotification.PublicKey, webPushNotification.PrivateKey);
            //var webPushClient = new WebPushClient();
            //try
            //{
            //    await _storageService.AddPushNotification(subscription);
            //    var payload = System.Text.Json.JsonSerializer.Serialize(new
            //    {
            //        message = "Ciao",
            //        url = $"myorders/10",
            //    });
            //    await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
            //    // Save pushSubscription
            //}
            //catch (Exception ex)
            //{
            //    Console.Error.WriteLine("Error sending push notification: " + ex.Message);
            //}
            return new OkResult();
        }

        [FunctionName("NotificationOnlyText")]
        public async Task<IActionResult> NotificationOnlyText(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var responseBody = await req.ReadAsStringAsync();
            NotificationMessageText messageText = JsonConvert.DeserializeObject<NotificationMessageText>(responseBody);
            var notifications = await _storageService.GetAllNotifications();
            var vapidDetails = new VapidDetails(webPushNotification.Subject, webPushNotification.PublicKey, webPushNotification.PrivateKey);
            foreach (var n in notifications)
            {
                var webPushClient = new WebPushClient();
                var pushSubscription = new PushSubscription(n.Url, n.P256dh, n.Auth);
                var payload = System.Text.Json.JsonSerializer.Serialize(messageText);
                await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
            }
            return new OkResult();
        }

        [FunctionName("NotificationWithImages")]
        public async Task<IActionResult> NotificationWithImages(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            var responseBody = await req.ReadAsStringAsync();
            NotificationImage messageImage = JsonConvert.DeserializeObject<NotificationImage>(responseBody);
            var notifications = await _storageService.GetAllNotifications();
            var vapidDetails = new VapidDetails(webPushNotification.Subject, webPushNotification.PublicKey, webPushNotification.PrivateKey);
            foreach (var n in notifications)
            {
                var webPushClient = new WebPushClient();
                var pushSubscription = new PushSubscription(n.Url, n.P256dh, n.Auth);
                var payload = System.Text.Json.JsonSerializer.Serialize(messageImage);
                try
                {
                    await webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                }
                catch (WebPushException) // Sottoscrizioni non più valide
                {
                    await _storageService.DeleteSubscription(n);
                }                
            }
            return new OkResult();
        }
    }
}
