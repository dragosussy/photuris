using Microsoft.EntityFrameworkCore;
using photuris_backend.DbContext.Entities;

namespace photuris_backend.DbContext
{
    public class Repository : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }

        public Repository(DbContextOptions<Repository> options) : base(options)
        {
        }
    }
}
