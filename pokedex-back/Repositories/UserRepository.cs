using Microsoft.EntityFrameworkCore;
using pokedex_back.Data;
using pokedex_back.DTOs;
using pokedex_back.DTOs.Auth;
using pokedex_back.DTOs.User;
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

        public async Task<bool> DeleteUser(long id)
        {
            try
            {
                var user =
                    await _context.Users.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new Exception("User not found");

                _context.Users.Remove(user);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserDTO> GetUserById(long id)
        {
            try
            {
                var user =
                    await _context
                        .Users.Select(x => new UserDTO
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Username = x.Username,
                        })
                        .FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new Exception("User not found");

                return user;
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

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            try
            {
                var users = await _context
                    .Users.Select(x => new UserDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Username = x.Username,
                    })
                    .ToListAsync();

                return users;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserDTO> UpdateUser(UpdateUserDTO user)
        {
            try
            {
                var userToUpdate =
                    await GetUserById(user.Id) ?? throw new Exception("User not found");

                var updatedUser = new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Username,
                    UpdatedAt = user.UpdatedAt,
                };

                if (!string.IsNullOrEmpty(user.Password))
                {
                    updatedUser.SetPassword(user.Password);
                }

                _context.Users.Update(updatedUser);
                await _context.SaveChangesAsync();

                return new UserDTO
                {
                    Id = updatedUser.Id,
                    Name = updatedUser.Name,
                    Username = updatedUser.Username,
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
