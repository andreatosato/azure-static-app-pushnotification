using Blazor.IndexedDB.Framework;
using Blazoring.PWA.Shared.Models;
using Microsoft.JSInterop;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blazoring.PWA.Client.Stores
{
    public class UserStore : IndexedDb
    {
        public UserStore(IJSRuntime jSRuntime, string name, int version) 
            : base(jSRuntime, name, version)
        {
        }

        public IndexedSet<UserEntity> Users { get; set; }
        public IndexedSet<CountryEntities> Countries { get; set; }
    }

    public class UserEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public Uri Picture { get; set; }
        public GenderEntity Gender { get; set; }
        public string UserDescription { get; set; }
        public string StreetAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        public decimal Balance { get; set; }
        public string JobTitle { get; set; }
        public string JobType { get; set; }
        public string PhoneNumber { get; set; }

        public User ToModel()
        {
            return new User(Id)
            {
                Balance = Balance,
                BirthDay = BirthDay,
                City = City,
                Country = Country,
                Email = Email,
                FirstName = FirstName,
                Gender = (Gender)Gender,
                JobTitle = JobTitle,
                JobType = JobTitle,
                LastName = LastName,
                Latitude = Latitude,
                Longitude = Longitude,
                PhoneNumber = PhoneNumber,
                Picture = Picture,
                State = State,
                StreetAddress = StreetAddress,
                UserDescription = UserDescription,
                UserName = UserName
            };
        }

        public void FromModel(User user)
        {
            Id = user.Id;
            Balance = Balance;
            BirthDay = user.BirthDay;
            City = user.City;
            Country = user.Country;
            Email = user.Email;
            FirstName = user.FirstName;
            Gender = (GenderEntity)user.Gender;
            JobTitle = user.JobTitle;
            JobType = user.JobTitle;
            LastName = user.LastName;
            Latitude = user.Latitude;
            Longitude = user.Longitude;
            PhoneNumber = user.PhoneNumber;
            Picture = user.Picture;
            State = user.State;
            StreetAddress = user.StreetAddress;
            UserDescription = user.UserDescription;
            UserName = user.UserName;
        }
    }

    public enum GenderEntity
    {
        Male,
        Female
    }

    public class CountryEntities
    {
        [Key]
        public string Name { get; set; }
        public Country ToModel()
        {
            return new Country() { Name = Name };
        }
        public void FromModel(Country country)
        {
            Name = country.Name;
        }
    }
}
