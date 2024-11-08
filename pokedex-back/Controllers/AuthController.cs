using Microsoft.AspNetCore.Mvc;
using pokedex_back.DTOs.Auth;
using pokedex_back.Repositories;

namespace pokedex_back.Controllers
{
    [Route("auth")]
    public class AuthController: Controller
    {
        private readonly AuthRepository _authRepository;

        public AuthController(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var data = await _authRepository.Login(login);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            try
            {
                await _authRepository.Register(register);
                var user = await _authRepository.Login(new LoginDTO
                {
                    Username = register.Username,
                    Password = register.Password
                });

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
