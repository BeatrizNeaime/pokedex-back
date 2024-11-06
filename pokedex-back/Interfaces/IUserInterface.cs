using pokedex_back.DTOs;
using pokedex_back.DTOs.Auth;
using pokedex_back.DTOs.User;
using pokedex_back.Models;

namespace pokedex_back.Interfaces
{
    public interface IUserInterface
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> CreateUser(RegisterDTO user);
        Task<User> GetUserByUserName(string userName);
        Task<UserDTO> GetUserById(long id);
        Task<UserDTO> UpdateUser(UpdateUserDTO user);
        Task<bool> DeleteUser(DeleteUserDTO user);
    }
}
