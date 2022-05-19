using Microsoft.EntityFrameworkCore;

namespace ApiPerson_Check.Models
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
    }
}
