using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AirboxTechTest.Models
{
    public class User
    {
        public int Id { get; set; }

        [JsonIgnore]
        public ICollection<Location>? Locations { get; set; }
    }
}
