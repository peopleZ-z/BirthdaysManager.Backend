using Birthdays.Api.Models;
using Birthdays.Api.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Birthdays.Api.Persistence
{
    public class BirthdaysDataContext : DbContext, IBirthdaysDataContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public BirthdaysDataContext(DbContextOptions<BirthdaysDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PersonConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
