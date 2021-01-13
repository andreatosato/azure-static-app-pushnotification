using Blazoring.PWA.Shared;
using System;
using Dapper;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq;

namespace Blazoring.PWA.API.Services
{
    public interface IStorageService
    {
        Task AddPushNotification(NotificationSubscription notification);
        Task<NotificationSubscription[]> GetAllNotifications();
        Task DeleteSubscription(NotificationSubscription notification);
    }

    public class StorageService : IStorageService
    {
        private readonly string connectionString;
        public StorageService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task AddPushNotification(NotificationSubscription notification)
        {
            string sqlNotificationUpsert =
                @" IF EXISTS ( SELECT * FROM dbo.NotificationSubscription WHERE Url = @Url )
                    UPDATE dbo.NotificationSubscription
                       SET P256dh = @P256dh, Auth = @Auth
                    WHERE Url = @Url 
                  ELSE 
                    INSERT INTO NotificationSubscription (Url, P256dh, Auth) Values (@Url, @P256dh, @Auth)";
            using (var connection = new SqlConnection(connectionString))
            {
                    await connection.ExecuteAsync(sqlNotificationUpsert, new { notification.Url, notification.P256dh, notification.Auth });
            }
        }

        public async Task DeleteSubscription(NotificationSubscription notification)
        {
            string sqlNotificationExist = "DELETE dbo.NotificationSubscription WHERE Url = @Url AND P256dh = @P256dh AND Auth = @Auth";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sqlNotificationExist, new { notification.Url, notification.P256dh, notification.Auth });
            }
        }

        public async Task<NotificationSubscription[]> GetAllNotifications()
        {
            NotificationSubscription[] notifications = null;
            string queryNotification = @"SELECT Url, P256dh, Auth FROM NotificationSubscription";
            using (var connection = new SqlConnection(connectionString))
            {
                notifications = (await connection.QueryAsync<NotificationSubscription>(queryNotification)).ToArray();
            }
            return notifications;
        }
    }
}
