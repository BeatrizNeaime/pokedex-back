using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokedex_back.DTOs.Pokemon
{
    public class CapturedPokemonsDTO
    {
        public string PokemonName { get; set; } = string.Empty;
        public string PokemonUrl { get; set; } = string.Empty;
        public UserDTO User { get; set; } = new UserDTO();
        public DateTime CapturedAt { get; set; } = DateTime.Now;
    }
}
