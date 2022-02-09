using load_testing_api.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace load_testing_api.Repository
{
    internal sealed class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}