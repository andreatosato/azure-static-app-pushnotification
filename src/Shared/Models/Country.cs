using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blazoring.PWA.Shared.Models
{
    public class Country
    {
        public string Name { get; set; }
        public static implicit operator Country(string c) => new Country() { Name = c };
        public static implicit operator string(Country c) => c.Name;
    }
}
