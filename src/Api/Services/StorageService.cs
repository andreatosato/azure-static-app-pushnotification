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
            string sqlNotificationExist = "SELECT 1 FROM NotificationSubscription WHERE Url = @Url";
            string sqlNotificationInsert = "INSERT INTO NotificationSubscription (Url, P256dh, Auth) Values (@Url, @P256dh, @Auth);";
            using (var connection = new SqlConnection(connectionString))
            {
                bool exist = await connection.ExecuteScalarAsync<bool>(sqlNotificationExist, new { notification.Url });
                if (!exist)
                {
                    await connection.ExecuteAsync(sqlNotificationInsert, new { notification.Url, notification.P256dh, notification.Auth });
                }                
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
