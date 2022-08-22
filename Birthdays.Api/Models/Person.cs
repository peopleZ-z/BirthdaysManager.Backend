namespace Birthdays.Api.Models
{
    public class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        public ICollection<Photo>? Photos { get; set; }

        public ICollection<Gift>? Gifts { get; set; }

    }
}
