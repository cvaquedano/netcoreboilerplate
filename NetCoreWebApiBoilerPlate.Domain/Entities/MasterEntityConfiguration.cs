using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace NetCoreWebApiBoilerPlate.Domain.Entities
{
    public class MasterEntityConfiguration : IEntityTypeConfiguration<ExampleMasterEntity>
    {
        public void Configure(EntityTypeBuilder<ExampleMasterEntity> builder)
        {
        
            builder.HasData(    
                new ExampleMasterEntity()
                {
                    Id = Guid.Parse("102b566b-ba1f-404c-b2df-e2cde39ade09"),
                    FirstName = "Carlos",
                    LastName = "Vaquedano",
                    Gender = true,
                    DOB = DateTime.Parse("1988-04-14"),
                    MasterStatusEntityId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96")
                }
            );
        }
    }
}
