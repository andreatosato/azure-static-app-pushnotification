using Bogus;
using System;
using Bogus.DataSets;
using Blazoring.PWA.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Blazoring.PWA.API.Services
{
    public interface ISeedGeneratorService
    {
        Country[] GetCountries();
        User[] GetUsers(int count);
        void SetUser(User u);
        void UpdateUser(User u);
    }

    public class BogusGeneratorService : ISeedGeneratorService
    {
        private readonly Lorem lorem;
        private const string language = "it";
        public BogusGeneratorService()
        {
            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(1111111);
            lorem = new Lorem(locale: language);
        }

        public Country[] GetCountries()
        {
            var countries = new Country[] { "Germany", "Italy", "Switzerland", "Sweden", "Argentina", "Republic of Ireland", 
                "Russia", "Brazil", "Norway", "Denmark", "England", "France", "Spain", "Mexico", "Netherlands", 
                "Nigeria", "Belgium", "Colombia", "Poland", "Romania", "Uruguay", "Portugal", "Cameroon", "Zambia", 
                "Egypt", "Wales", "USA", "Côte d'Ivoire", "Scotland", "Tunisia", "Greece", "Morocco", "Algeria", "Ecuador", 
                "Korea Republic", "Bulgaria", "Austria", "Costa Rica", "Northern Ireland", "Honduras", "Hungary", "Japan", 
                "Saudi Arabia", "China", "Canada", "Finland", "Ghana", "Chile", "Zimbabwe", "Qatar", "Australia" };
            return countries;
        }

        private List<User> _users = new List<User>();

        public User[] GetUsers(int count)
        {
            if (!_users.Any())
            {
                var userIds = 0;
                var countries = GetCountries();
                var faker = new Faker<User>(language)
                    .CustomInstantiator(f => new User(userIds++))
                    .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
                    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
                    .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                    .RuleFor(u => u.FullName, (f, u) => $"{u.FirstName} {u.LastName}")
                    .RuleFor(u => u.BirthDay, (f, u) => f.Date.Past(18, DateTime.UtcNow.AddYears(-100)))
                    .RuleFor(u => u.City, (f, u) => f.Address.City())
                    .RuleFor(u => u.Country, (f, u) => f.PickRandom(countries))
                    .RuleFor(u => u.Latitude, (f, u) => f.Address.Latitude())
                    .RuleFor(u => u.Longitude, (f, u) => f.Address.Longitude())
                    .RuleFor(u => u.Picture, (f, u) => new Uri(f.Image.LoremFlickrUrl(keywords: $"{u.Country.Name}", width: 800, height: 600)))
                    .RuleFor(u => u.State, (f, u) => f.Address.State())
                    .RuleFor(u => u.StreetAddress, (f, u) => f.Address.StreetAddress())
                    .RuleFor(u => u.UserDescription, (f, u) => lorem.Paragraphs(f.Random.Int(1, 3), separator: "\n\n"))
                    .RuleFor(u => u.Balance, (f, u) => f.Random.Decimal(-10_000m, 1_000_000m))
                    .RuleFor(u => u.JobTitle, (f, u) => f.Name.JobTitle())
                    .RuleFor(u => u.JobType, (f, u) => f.Name.JobType())
                    .RuleFor(u => u.PhoneNumber, (f, u) => f.Phone.PhoneNumber());

                User[] result = new User[count];
                for (int i = 0; i < count; i++)
                {
                    result[i] = faker.Generate();
                }
                _users = result.ToList();
            }
            return _users.ToArray();
        }

        public void SetUser(User u)
        {
            _users.Add(u);
        }

        public void UpdateUser(User u)
        {
            var userToUpdate = _users.Find(t => t.Id == u.Id);
            if(userToUpdate != null)
            {
                _users.Remove(userToUpdate);
                _users.Add(u);
            }
            
        }
    }
}
