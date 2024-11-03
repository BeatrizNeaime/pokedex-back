using pokedex_back.DTOs;
using pokedex_back.DTOs.Auth;
using pokedex_back.Models;

namespace pokedex_back.Interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<User>> GetUsers();
        Task<UserDTO> CreateUser(RegisterDTO user);
        Task<User> GetUserByUserName(string userName);
    }
}
