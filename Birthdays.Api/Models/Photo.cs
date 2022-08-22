namespace Birthdays.Api.Models
{
    public class Photo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public Person? Person { get; set; }

        public Guid? PersonId { get; set; }
    }
}
