using Blazor.IndexedDB.Framework;
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
    }

    public class UserEntity
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
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
        public string PhoneNumber { get; set; }
    }

    public enum GenderEntity
    {
        Male,
        Female
    }
}
