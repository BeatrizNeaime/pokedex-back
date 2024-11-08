using Microsoft.IdentityModel.Tokens;
using pokedex_back.Data;
using pokedex_back.DTOs;
using pokedex_back.DTOs.Auth;
using pokedex_back.Interfaces;
using pokedex_back.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace pokedex_back.Repositories
{
    public class AuthRepository: IAuthInterface
    {
        private readonly Context _context;
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthRepository(Context context, IConfiguration configuration)
        {
            _context = context;
            _userRepository = new UserRepository(context);
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            try
            {
                var claims = new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
                );

                var token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(4),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserDTO> Login(LoginDTO login)
        {
            try
            {
                var user =
                    await _userRepository.GetUserByUserName(login.Username)
                    ?? throw new Exception("User not found");

                if (!user.CheckPassword(login.Password))
                {
                    throw new Exception("Invalid password");
                }

                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name,
                    Token = GenerateToken(user),
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<UserDTO> Register(RegisterDTO register)
        {
            try
            {
                return await _userRepository.CreateUser(register);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
