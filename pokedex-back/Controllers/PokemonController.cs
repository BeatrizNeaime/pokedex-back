using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pokedex_back.DTOs.Pokemon;
using pokedex_back.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace pokedex_back.Controllers
{
    [Route("pokemon")]
    public class PokemonController : Controller
    {
        private readonly PokemonRepository _pokemonRepository;

        public PokemonController(PokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("capture")]
        public async Task<IActionResult> CapturePokemon([FromBody] CapturePokemonDTO capture)
        {
            try
            {
                var pokemon = await _pokemonRepository.CapturePokemon(capture);
                return Ok(pokemon);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("release")]
        public async Task<IActionResult> ReleasePokemon([FromBody] ReleasePokemonDTO release)
        {
            try
            {
                var pokemon = await _pokemonRepository.ReleasePokemon(release);
                return Ok(pokemon);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet]
        [Route("captured")]
        public async Task<IActionResult> GetCapturedPokemons()
        {
            try
            {
                var pokemons = await _pokemonRepository.GetCapturedPokemons();
                return Ok(pokemons);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("captured-by/{id}")]
        public async Task<IActionResult> GetCapturedByUser(long id)
        {
            try
            {
                var pokemon = await _pokemonRepository.GetCapturedPokemonsByUser(id);
                return Ok(pokemon);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
