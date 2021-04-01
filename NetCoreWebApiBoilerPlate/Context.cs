using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Entities;

namespace NetCoreWebApiBoilerPlate
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
           : base(options)
        {

        }

        public DbSet<ExampleMasterEntity> ExampleMasterEntity { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
