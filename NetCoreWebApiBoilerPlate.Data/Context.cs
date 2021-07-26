using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using System;
using EasyEncryption;

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
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Carlos",
                    LastName = "Vaquedano",
                    Email="chvb2002@gmail.com",
                    Username="cvaquedano",
                    Status=1,
                    Password = SHA.ComputeSHA256Hash("chvb2002")
                }
                );

            modelBuilder.Entity<MasterStatusEntity>().HasData(
              new MasterStatusEntity()
              {
                  Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                  Value= "Active",
                  Description= "An active entity"
              },
              new MasterStatusEntity()
              {
                  Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                  Value = "Inactive",
                  Description = "An Inactive entity"
              }
              );

            modelBuilder.Entity<ExampleMasterEntity>().HasData(
            new ExampleMasterEntity()
            {
                Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
               FirstName="Carlos",
               LastName="Vaquedano",
               Gender = true,
               DOB = DateTime.Parse("1988-04-14"),
               MasterStatusEntityId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96")
            }
            );

            modelBuilder.Entity<MasterDetailEntity>().HasData(
           new MasterDetailEntity()
           {
               Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
              ExampleMasterEntityId = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
              Value="Detail Value",
              Price = 1,
              Quantity=10,
              Total=100
           }
           );
            base.OnModelCreating(modelBuilder);
        }
    }
}
