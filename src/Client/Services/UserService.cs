using Blazor.IndexedDB.Framework;
using Blazoring.PWA.Client.Stores;
using Blazoring.PWA.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazoring.PWA.Client.Services
{
    public interface IUserService
    {
        Task<User[]> GetUserAsync();

    }

    public class UserService : IUserService
    {
        private readonly HttpClient client;
        private Lazy<Task<UserStore>> lazyDb;

        public UserService(HttpClient client, IIndexedDbFactory dbFactory)
        {
            this.client = client;
            lazyDb = new Lazy<Task<UserStore>>(async () => await dbFactory.Create<UserStore>());
        }

        public async Task<User[]> GetUserAsync()
        {
            User[] users = null;
            using var db = await lazyDb.Value;
            {
                if (db.Users.Count == 0)
                {
                    users = await client.GetFromJsonAsync<User[]>("SeedUsers").ConfigureAwait(false);
                }
            }
            return users;
        }
    }
}
