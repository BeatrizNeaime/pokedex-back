using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pokedex_back.Repositories;

namespace pokedex_back.Controllers
{
    [Route("/user")]
    public class UserController(UserRepository userRepository) : Controller
    {
        private readonly UserRepository _userRepository = userRepository;

        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
