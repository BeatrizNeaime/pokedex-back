using pokedex_back.DTOs.Pokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokedex_back.Interfaces
{
    public interface IPokemonInterface
    {
        Task<CapturedPokemonsDTO> CapturePokemon(CapturePokemonDTO capture);
        Task<IEnumerable<CapturedPokemonsDTO>> GetCapturedPokemons();
        Task<bool> CheckPokemonExists(string pokemonName);
        Task<IEnumerable<CapturedPokemonsDTO>> GetCapturedPokemonsByUser(long id);
        Task<ReleasePokemonDTO> ReleasePokemon(ReleasePokemonDTO release);
    }
}
