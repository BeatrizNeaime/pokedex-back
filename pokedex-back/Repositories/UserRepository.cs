using Microsoft.EntityFrameworkCore;
using pokedex_back.Data;
using pokedex_back.DTOs;
using pokedex_back.DTOs.Auth;
using pokedex_back.Interfaces;
using pokedex_back.Models;

namespace pokedex_back.Repositories
{
    public class UserRepository(Context context) : IUserInterface
    {
        private readonly Context _context = context;

        public async Task<UserDTO> CreateUser(RegisterDTO user)
        {
            try
            {
                var previousUser = await GetUserByUserName(user.Username);

                if (previousUser != null)
                {
                    throw new Exception("Username already exists");
                }

                var newUser = new User { Name = user.Name, Username = user.Username };

                newUser.SetPassword(user.Password);

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return new UserDTO
                {
                    Id = newUser.Id,
                    Name = newUser.Name,
                    Username = newUser.Username,
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<User> GetUserByUserName(string userName)
        {
            try
            {
                return _context.Users.FirstOrDefaultAsync(x => x.Username == userName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
