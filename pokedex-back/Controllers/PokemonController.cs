using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pokedex_back.DTOs.Pokemon;
using pokedex_back.Repositories;

namespace pokedex_back.Controllers
{
    [Route("pokemon")]
    public class PokemonController(PokemonRepository pokemonRepository) : Controller
    {
        private readonly PokemonRepository _pokemonRepository = pokemonRepository;

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
                return BadRequest(e.Message);
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
                return BadRequest(e.Message);
            }
        }
    }
}
