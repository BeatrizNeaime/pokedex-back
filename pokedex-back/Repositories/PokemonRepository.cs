using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using pokedex_back.Data;
using pokedex_back.DTOs;
using pokedex_back.DTOs.Pokemon;
using pokedex_back.Hubs;
using pokedex_back.Interfaces;
using pokedex_back.Models;

namespace pokedex_back.Repositories
{
    public class PokemonRepository : IPokemonInterface
    {
        private readonly Context _context;
        private readonly UserRepository _userRepository;
        private readonly IHubContext<PokemonHub> _hubContext;

        public PokemonRepository(
            Context context,
            UserRepository userRepository,
            IHubContext<PokemonHub> hubContext
        )
        {
            _context = context;
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        public Task<CapturedPokemonsDTO> CapturePokemon(CapturePokemonDTO capture)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return strategy.ExecuteAsync(async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var user =
                        await _userRepository.GetUserById(capture.UserId)
                        ?? throw new Exception("User not found");

                    var pokemonExists = await CheckPokemonExists(capture.PokemonName);
                    if (pokemonExists)
                    {
                        throw new InvalidOperationException("Pokemon already captured");
                    }

                    if (await GetCapturedPokemonsByUser(user.Id) >= 3)
                    {
                        throw new InvalidOperationException("User already captured 3 pokemons");
                    }

                    var capturedPokemon = new CapturedPokemon
                    {
                        UserId = user.Id,
                        PokemonName = capture.PokemonName,
                    };

                    await _context.CapturedPokemons.AddAsync(capturedPokemon);
                    await _context.SaveChangesAsync();

                    await _hubContext.Clients.All.SendAsync(
                        "PokemonCaptured",
                        user.Id,
                        capture.PokemonName,
                        capturedPokemon.CapturedAt
                    );

                    await transaction.CommitAsync();

                    return new CapturedPokemonsDTO
                    {
                        PokemonName = capturedPokemon.PokemonName,
                        CapturedAt = capturedPokemon.CapturedAt,
                        User = new UserDTO { Username = user.Username },
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(e.ToString());
                }
            });
        }

        public async Task<bool> CheckPokemonExists(string pokemonName)
        {
            try
            {
                return await _context.CapturedPokemons.AnyAsync(x => x.PokemonName == pokemonName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<CapturedPokemonsDTO>> GetCapturedPokemons()
        {
            try
            {
                return await _context
                    .CapturedPokemons.Join(
                        _context.Users,
                        capturedPokemon => capturedPokemon.UserId,
                        user => user.Id,
                        (capturedPokemon, user) =>
                            new
                            {
                                capturedPokemon.PokemonName,
                                capturedPokemon.CapturedAt,
                                user.Username,
                            }
                    )
                    .Select(x => new CapturedPokemonsDTO
                    {
                        PokemonName = x.PokemonName,
                        CapturedAt = x.CapturedAt,
                        User = new UserDTO { Username = x.Username },
                    })
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> GetCapturedPokemonsByUser(long id)
        {
            try
            {
                return await _context.CapturedPokemons.CountAsync(x => x.UserId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ReleasePokemon(ReleasePokemonDTO release)
        {
            try
            {
                var capture =
                    await _context.CapturedPokemons.FirstOrDefaultAsync(x =>
                        x.PokemonName == release.PokemonName && x.UserId == release.UserId
                    ) ?? throw new Exception("Pokemon not found");

                _context.CapturedPokemons.Remove(capture);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
