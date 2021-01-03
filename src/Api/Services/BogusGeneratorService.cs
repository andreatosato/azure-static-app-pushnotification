using Bogus;
using System;
using Bogus.DataSets;
using Blazoring.PWA.Shared.Models;

namespace Blazoring.PWA.API.Services
{
    public interface ISeedGeneratorService
    {
        User[] GetUsers(int count);
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

        public User[] GetUsers(int count)
        {
            var userIds = 0;
            var faker = new Faker<User>(language)
                .CustomInstantiator(f => new User(userIds++))
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.FullName, (f, u) => $"{u.FirstName} {u.LastName}")
                .RuleFor(u => u.BirthDay, (f,u) => f.Date.Past(18, DateTime.UtcNow.AddYears(-100)))
                .RuleFor(u => u.City, (f,u) => f.Address.City())
                .RuleFor(u => u.Country, (f, u) => f.Address.Country())
                .RuleFor(u => u.Latitude, (f, u) => f.Address.Latitude())
                .RuleFor(u => u.Longitude, (f, u) => f.Address.Longitude())
                .RuleFor(u => u.Picture, (f, u) => new Uri(f.Image.LoremFlickrUrl(keywords: $"{u.Country}", width: 800, height: 600)))
                .RuleFor(u => u.State, (f,u) => f.Address.State())
                .RuleFor(u => u.StreetAddress, (f, u) => f.Address.StreetAddress())
                .RuleFor(u => u.UserDescription, (f, u) => lorem.Paragraphs(f.Random.Int(2,7), separator: "<br>"))
                .RuleFor(u => u.Balance, (f, u) => f.Random.Decimal(-10_000m, 1_000_000m))
                .RuleFor(u => u.JobTitle, (f, u) => f.Name.JobTitle())
                .RuleFor(u => u.PhoneNumber, (f, u) => f.Phone.PhoneNumber());

            User[] result = new User[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = faker.Generate();
            }
            return result;
        }
    }
}
