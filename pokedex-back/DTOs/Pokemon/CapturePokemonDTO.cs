using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokedex_back.DTOs.Pokemon
{
    public class CapturePokemonDTO
    {
        public long UserId { get; set; }
        public string PokemonName { get; set; } = string.Empty;
    }
}
