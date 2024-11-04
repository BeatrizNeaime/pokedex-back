using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pokedex_back.DTOs;
using pokedex_back.DTOs.User;
using pokedex_back.Models;
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

        [Authorize]
        [HttpPatch]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO user)
        {
            try
            {
                var updatedUser = await _userRepository.UpdateUser(user);
                return Ok(updatedUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser([FromBody] long id)
        {
            try
            {
                await _userRepository.DeleteUser(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
