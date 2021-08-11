using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using EasyEncryption;

namespace NetCoreWebApiBoilerPlate.Domain.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Carlos",
                    LastName = "Vaquedano",
                    Email = "chvb2002@gmail.com",
                    Username = "cvaquedano",
                    Status = 1,
                    Password = SHA.ComputeSHA256Hash("chvb2002")
                }
                );
        }
    }
}
