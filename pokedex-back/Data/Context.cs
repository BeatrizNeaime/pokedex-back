using Microsoft.EntityFrameworkCore;
using pokedex_back.Models;

namespace pokedex_back.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CapturedPokemon> CapturedPokemons { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<CapturedPokemon>().HasIndex(u => u.PokemonName).IsUnique();
        }
    }
}
