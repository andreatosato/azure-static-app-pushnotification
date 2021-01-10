using Blazor.IndexedDB.Framework;
using Blazoring.PWA.Client.Stores;
using Blazoring.PWA.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Blazoring.PWA.Client.Services
{
    public interface IUserService
    {
        Task<Country[]> GetCountriesAsync();
        Task<User[]> GetUserAsync();
        Task<User> GetUserByIdAsync(int id);
        Task SaveUserAsync(User user);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient client;
        private Lazy<Task<UserStore>> lazyDb;
        private ILogger<UserService> _logger;

        public UserService(HttpClient client, IIndexedDbFactory dbFactory, ILogger<UserService> logger)
        {
            this.client = client;
            lazyDb = new Lazy<Task<UserStore>>(async () => await dbFactory.Create<UserStore>());
            _logger = logger;
        }

        public async Task<Country[]> GetCountriesAsync()
        {
            Country[] countries = null;
            using var db = await lazyDb.Value;
            {
                if(db.Countries.Count == 0)
                {
                    countries = await client.GetFromJsonAsync<Country[]>("SeedCountries").ConfigureAwait(false);
                    if (countries.Length >= 0)
                    {
                        foreach (var c in countries)
                        {
                            CountryEntities newEntity = new CountryEntities();
                            newEntity.FromModel(c);
                            db.Countries.Add(newEntity);
                        }
                        await db.SaveChanges();
                    }
                }
                else
                {
                    countries = db.Countries.Select(x => x.ToModel()).ToArray();
                }
                return countries;
            }
        }

        public async Task<User[]> GetUserAsync()
        {
            try
            {
                User[] users = null;
                using var db = await lazyDb.Value;
                {
                    if (db.Users.Count == 0)
                    {
                        users = await client.GetFromJsonAsync<User[]>("SeedUsers").ConfigureAwait(false);

                        if (users.Length >= 0)
                        {
                            foreach (var u in users)
                            {
                                UserEntity newEntity = new UserEntity();
                                newEntity.FromModel(u);
                                db.Users.Add(newEntity);
                            }
                            await db.SaveChanges();
                        }
                    }
                    else
                    {
                        users = db.Users.Select(x => x.ToModel()).ToArray();
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore User Service");
            }
            return null;
        }

        /// <summary>
        /// Il metodo per la ricerca dell'utente lo possiamo fare sull'indexdb locale.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(int id)
        {
            User result = null;
            using var db = await lazyDb.Value;
            {
                var found = db.Users.FirstOrDefault(t => t.Id == id);
                if(found != null)
                {
                    result = found.ToModel();
                }
            }
            return result;
        }

        public async Task SaveUserAsync(User user)
        {
            using var db = await lazyDb.Value;
            {
                var dbUser = db.Users.Where(t => t.Id == user.Id).FirstOrDefault();
                if(dbUser != null)
                {
                    db.Users.Remove(dbUser);
                }
                var newEntity = new UserEntity();
                newEntity.FromModel(user);
                db.Users.Add(newEntity);
                await db.SaveChanges().ConfigureAwait(false);
            }
        }
    }
}