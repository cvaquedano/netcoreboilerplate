using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace NetCoreWebApiBoilerPlate.Domain.Entities
{
    public class MasterDetailConfiguration : IEntityTypeConfiguration<MasterDetailEntity>
    {
        public void Configure(EntityTypeBuilder<MasterDetailEntity> builder)
        {
            builder.HasData(
         new MasterDetailEntity()
         {
             Id = Guid.Parse("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"),
             ExampleMasterEntityId = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
             Value = "Detail Value",
             Price = 1,
             Quantity = 10,
             Total = 100
         }
         );
        }
    }
}
