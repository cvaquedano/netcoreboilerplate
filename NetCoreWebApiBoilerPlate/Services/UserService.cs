using EasyEncryption;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using NetCoreWebApiBoilerPlate.Repositories;
using NetCoreWebApiBoilerPlate.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public class UserService : IUserService
    {  
        private readonly AppSettings _appSettings;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork)
        {
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto model)
        {
            var user = await _unitOfWork.UserRepository.Authenticate(model.Username, model.Email);
            // return null if user not found
            if (user == null) return null;

            if (!PasswordsMatch(model.Password,user.Password)) return null;
          

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponseDto(user, token);
        }

        public async Task DeleteAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _unitOfWork.UserRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        //public async Task<PagedList<User>> GetAllAsync(UsersRequestDto requestDto)
        //{
        //    if (requestDto is null)
        //    {
        //        throw new ArgumentNullException(nameof(requestDto));
        //    }

        //    var collection = await Task.FromResult(_unitOfWork.UserRepository.GetAll());

        //    if (!string.IsNullOrWhiteSpace(requestDto.SearchQuery))
        //    {
        //        requestDto.SearchQuery = requestDto.SearchQuery.Trim();
        //        collection = collection.Where(a => a.FirstName.Contains(requestDto.SearchQuery)
        //        || a.LastName.Contains(requestDto.SearchQuery));
        //    }
        //    return PagedList<User>.Create(collection, requestDto.PageNumber, requestDto.PageSize);
        //}

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _unitOfWork.UserRepository.IsExistsAsync(id);
        }


        public async Task UpdateAsync(User entity)
        {
            if (!RegexUtilities.IsValidEmail(entity.Email))
            {
                return;
            }
            _unitOfWork.UserRepository.Update(entity);
            await _unitOfWork.SaveAsync();
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

        public async Task<PagedList<User>> GetAllAsync(PaginationRequestBaseDto requestDto)
        {
            if (requestDto is null)
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            var collection = await Task.FromResult(_unitOfWork.UserRepository.GetAll());

            if (!string.IsNullOrWhiteSpace(requestDto.SearchQuery))
            {
                requestDto.SearchQuery = requestDto.SearchQuery.Trim();
                collection = collection.Where(a => a.FirstName.Contains(requestDto.SearchQuery)
                || a.LastName.Contains(requestDto.SearchQuery));
            }
            return PagedList<User>.Create(collection, requestDto.PageNumber, requestDto.PageSize);
        }

        public async Task AddAsync(User entity)
        {
            if (!RegexUtilities.IsValidEmail(entity.Email))
            {
                return;
            }
            entity.Id = Guid.NewGuid();
            entity.Password = HashPassword(entity.Password);
            await _unitOfWork.UserRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}
