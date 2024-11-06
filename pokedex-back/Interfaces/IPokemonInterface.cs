using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pokedex_back.DTOs.Pokemon;

namespace pokedex_back.Interfaces
{
    public interface IPokemonInterface
    {
        Task<CapturedPokemonsDTO> CapturePokemon(CapturePokemonDTO capture);
        Task<IEnumerable<CapturedPokemonsDTO>> GetCapturedPokemons();
        Task<bool> CheckPokemonExists(string pokemonName);
        Task<int> GetCapturedPokemonsByUser(long id);
        Task<bool> ReleasePokemon(ReleasePokemonDTO release);
    }
}
