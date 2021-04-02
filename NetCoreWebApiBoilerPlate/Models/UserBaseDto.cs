using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using System;

namespace NetCoreWebApiBoilerPlate.Models
{
    public class UserBaseDto : ReadBaseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
    }
}
