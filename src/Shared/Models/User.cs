using System;
using System.Collections.Generic;
using System.Text;

namespace Blazoring.PWA.Shared.Models
{
    public class User
    {
        public User(int userId)
        {
            this.Id = userId;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
        public Gender Gender { get; set; }
        public string UserDescription { get; set; }
        public string StreetAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal Balance { get; set; }
        public string JobTitle { get; set; }
        public string PhoneNumber { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
