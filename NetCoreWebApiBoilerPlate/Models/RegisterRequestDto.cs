﻿namespace NetCoreWebApiBoilerPlate.Models
{
    public class RegisterRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
    }
}