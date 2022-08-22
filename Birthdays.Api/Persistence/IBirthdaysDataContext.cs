using Birthdays.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Birthdays.Api.Persistence
{
    public interface IBirthdaysDataContext
    {
        DbSet<Person> Persons { get; set; }

        DbSet<Gift> Gifts { get; set; }

        DbSet<Photo> Photos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
