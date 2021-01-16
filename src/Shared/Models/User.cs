using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blazoring.PWA.Shared.Models
{
    public class User : ICloneable
    {
        public User(int userId)
        {
            this.Id = userId;
        }
        // Serialization Ctor
        public User()
        {
        }

        [Required]
        [Range(int.MinValue, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Surname is too long.")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email.")]
        public string Email { get; set; }
        public Uri Picture { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string UserDescription { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 1)]
        public string StreetAddress { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public Country Country { get; set; } = new Country();
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal Balance { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string JobTitle { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string JobType { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public object Clone()
        {
            return new User
            {
                Id = Id,
                Balance = Balance,
                BirthDay = BirthDay,
                City = City,
                Country = Country,
                Email = Email,
                FirstName = FirstName,
                Gender = Gender,
                JobTitle = JobTitle,
                JobType = JobType,
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

        public User Clear()
        {
            return new User
            {
                Id = Id
            };
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
