using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using pokedex_back.DTOs;

namespace pokedex_back.Models
{
    [Table("capturedPokemons")]
    public class CapturedPokemon
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("pokemonName")]
        public string PokemonName { get; set; } = string.Empty;

        [Required]
        [Column("pokemonUrl")]
        public string PokemonUrl { get; set; } = string.Empty;

        [Required]
        [Column("userId")]
        public long UserId { get; set; }

        [Required]
        [Column("capturedAt")]
        public DateTime CapturedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

    }
}
