using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Domain.Entities;

namespace NetCoreWebApiBoilerPlate.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
           : base(options)
        {

        }

        public DbSet<ExampleMasterEntity> ExampleMasterEntity { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<MasterStatusEntity> MasterStatusEntity { get; set; }

        public DbSet<MasterDetailEntity> MasterDetailEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.ApplyConfiguration(new MasterStatusConfiguration());

            modelBuilder.ApplyConfiguration(new MasterEntityConfiguration());

            modelBuilder.ApplyConfiguration(new MasterDetailConfiguration());
          
            base.OnModelCreating(modelBuilder);
        }
    }
}
