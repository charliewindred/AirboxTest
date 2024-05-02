using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AirboxTechTest.Models
{
    public class Location
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
