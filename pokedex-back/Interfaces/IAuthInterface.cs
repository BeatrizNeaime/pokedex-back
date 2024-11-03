using pokedex_back.DTOs;
using pokedex_back.DTOs.Auth;
using pokedex_back.Models;

namespace pokedex_back.Interfaces
{
    public interface IAuthInterface
    {
        public string GenerateToken(User user);
        Task<UserDTO> Login(LoginDTO login);
        Task<UserDTO> Register(RegisterDTO register);
    }
}
