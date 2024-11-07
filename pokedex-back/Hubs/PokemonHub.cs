using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using pokedex_back.DTOs.Pokemon;
using pokedex_back.Repositories;

namespace pokedex_back.Hubs
{
    public class PokemonHub : Hub
    {
        private readonly PokemonRepository _pokemonRepository;

        public PokemonHub(PokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task CapturePokemon(CapturePokemonDTO capturePokemonDTO)
        {
            try
            {
                var captured = await _pokemonRepository.CapturePokemon(capturePokemonDTO);
                if (captured.PokemonName != null)
                {
                    await Clients.All.SendAsync("PokemonCaptured", captured);
                }
            }
            catch (Exception e)
            {
                await Clients.All.SendAsync("CapturePokemonFailed", new { message = $"Someone tried to capture {capturePokemonDTO.PokemonName}!" });
                await Clients.Group(capturePokemonDTO.UserId.ToString()).SendAsync(
                        "PokemonNotCaptured",
                        new { message = e.Message }
                );
            }
        }

        public async Task ReleasePokemon(ReleasePokemonDTO release)
        {
            try
            {
                var released = await _pokemonRepository.ReleasePokemon(release);
                if (!released.PokemonName.IsNullOrEmpty())
                {
                    await Clients.All.SendAsync("PokemonReleased", released);
                }

            }
            catch (Exception e)
            {
                await Clients.All.SendAsync("ReleasePokemonFailed", new { message = $"Someone tried to release {release.PokemonName}!" });
                await Clients.Group(release.UserId.ToString()).SendAsync("PokemonNotReleased", new { message = e.Message });
            }

        }
    }
}
