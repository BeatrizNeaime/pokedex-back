using Microsoft.AspNetCore.SignalR;
using pokedex_back.DTOs.Pokemon;
using pokedex_back.Repositories;

namespace pokedex_back.Hubs
{
    public class PokemonHub : Hub
    {
        private readonly PokemonRepository _pokemonRepository;
        private readonly ILogger<PokemonHub> _logger;

        public PokemonHub(PokemonRepository pokemonRepository, ILogger<PokemonHub> logger)
        {
            _pokemonRepository = pokemonRepository;
            _logger = logger;
        }

        public async Task CapturePokemon(CapturePokemonDTO capturePokemonDTO)
        {
            try
            {
                _logger.LogInformation(
                    "Capture attempt: User: {UserId}, Pokemon: {PokemonName}",
                    capturePokemonDTO.UserId,
                    capturePokemonDTO.PokemonName
                );

                var captured = await _pokemonRepository.CapturePokemon(capturePokemonDTO);
                if (captured.PokemonName != null)
                {
                    await Clients.Caller.SendAsync("PokemonCaptured", captured);
                }
                else
                {
                    await Clients.Caller.SendAsync(
                        "PokemonNotCaptured",
                        new { message = "Pokemon not captured" }
                    );
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error capturing Pokemon for User: {UserId}", capturePokemonDTO.UserId);

                await Clients.Caller.SendAsync("CapturePokemonFailed", new { message = "An error occurred on the server while capturing the Pokémon." });
            }
        }

    }
}
