using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace NetCoreWebApiBoilerPlate.Domain.Entities
{
    public class MasterStatusConfiguration : IEntityTypeConfiguration<MasterStatusEntity>
    {
        public void Configure(EntityTypeBuilder<MasterStatusEntity> builder)
        {
            builder.HasData(
               new MasterStatusEntity()
               {
                   Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                   Value = "Active",
                   Description = "An active entity"
               },
               new MasterStatusEntity()
               {
                   Id = Guid.Parse("2902b665-1190-4c70-9915-b9c2d7680450"),
                   Value = "Inactive",
                   Description = "An Inactive entity"
               }
               );
        }
    }
}
