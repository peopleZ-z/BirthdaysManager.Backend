using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Birthdays.Api.Models
{
    public class Gift
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Person>? Persons { get; set; }

    }
}
