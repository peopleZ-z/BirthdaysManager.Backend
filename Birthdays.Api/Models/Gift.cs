using System.ComponentModel.DataAnnotations;

namespace Birthdays.Api.Models
{
    public class Gift
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<Person>? Persons { get; set; }

    }
}
