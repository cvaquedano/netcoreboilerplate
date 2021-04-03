using EasyEncryption;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NetCoreWebApiBoilerPlate.Services
{
    public class UserService : IUserService
    {  
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public AuthenticateResponseDto Authenticate(AuthenticateRequestDto model)
        {
            var user = _userRepository.Authenticate(model.Username, model.Email);
            // return null if user not found
            if (user == null) return null;

            if (!PasswordsMatch(model.Password,user.Password)) return null;
          

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponseDto(user, token);
        }

        public void Delete(User userEntity)
        {
            if (userEntity == null)
            {
                throw new ArgumentNullException(nameof(userEntity));
            }
            _userRepository.Delete(userEntity);
            _userRepository.Save();
        }

        public PagedList<User> GetAll(UsersRequestDto usersRequestDto)
        {
            if (usersRequestDto is null)
            {
                throw new ArgumentNullException(nameof(usersRequestDto));
            }

            var collection = _userRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(usersRequestDto.SearchQuery))
            {
                usersRequestDto.SearchQuery = usersRequestDto.SearchQuery.Trim();
                collection = collection.Where(a => a.FirstName.Contains(usersRequestDto.SearchQuery)
                || a.LastName.Contains(usersRequestDto.SearchQuery));
            }
            return PagedList<User>.Create(collection, usersRequestDto.PageNumber, usersRequestDto.PageSize);
        }

        public User GetById(Guid id)
        {
            return _userRepository.GetById(id);
        }

        public bool IsEntityExist(Guid userId)
        {
            return _userRepository.IsExists(userId);
        }

        public void Register(User userEntity)
        {
            if (!RegexUtilities.IsValidEmail(userEntity.Email))
            {
                return;
            }
            userEntity.Id = Guid.NewGuid();
            userEntity.Password = HashPassword(userEntity.Password);
            _userRepository.Add(userEntity);
            _userRepository.Save();
        }

        public void Update(User userEntity)
        {
            if (!RegexUtilities.IsValidEmail(userEntity.Email))
            {
                return;
            }
            _userRepository.Update(userEntity);
            _userRepository.Save();
        }
              
        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string HashPassword(string input)
        { // Create a function to easily hash passwords
            return SHA.ComputeSHA256Hash(input); // One function to hash using SHA256 with EasyEncryption library, returns a string.
        }

        private static bool PasswordsMatch(string userInput, string savedTextFilePassword)
        { // Function to check if the user input the correct password
            string hashedInput = HashPassword(userInput); // Hash user input to check it against the one stored in a file
            bool doPasswordsMatch = string.Equals(hashedInput, savedTextFilePassword); // Check both passwords
            return doPasswordsMatch; // Return the result of comparing both hashed strings
        }
    }
}
