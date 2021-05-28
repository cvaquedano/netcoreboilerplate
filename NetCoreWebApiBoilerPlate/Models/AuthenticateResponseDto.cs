

namespace NetCoreWebApiBoilerPlate.Models
{
    public class AuthenticateResponseDto
    {
      
        public string Token { get; set; }

      

        public AuthenticateResponseDto(string token)
        {
         
            Token = token;
        }
        public AuthenticateResponseDto()
        {

        }
    }
}
