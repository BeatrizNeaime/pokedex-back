using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pokedex_back.Data;
using pokedex_back.DTOs;
using pokedex_back.DTOs.Pokemon;
using pokedex_back.Interfaces;
using pokedex_back.Models;

namespace pokedex_back.Repositories
{
    public class PokemonRepository(Context context, UserRepository userRepository)
        : IPokemonInterface
    {
        private readonly Context _context = context;
        private readonly UserRepository _userRepository = userRepository;

        public async Task<bool> CapturePokemon(CapturePokemonDTO capture)
        {
            try
            {
                var user =
                    await _userRepository.GetUserById(capture.UserId)
                    ?? throw new Exception("User not found");

                var pokemon = await CheckPokemonExists(capture.PokemonName);

                if (pokemon)
                {
                    throw new Exception("Pokemon already captured");
                }

                var captured = await GetCapturedPokemonsByUser(user.Id);

                if (captured.Count() >= 3)
                {
                    throw new Exception("User already has 3 pokemons");
                }

                var capturedPokemon = new CapturedPokemon
                {
                    UserId = user.Id,
                    PokemonName = capture.PokemonName,
                    PokemonUrl = capture.PokemonUrl,
                };

                await _context.CapturedPokemons.AddAsync(capturedPokemon);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> CheckPokemonExists(string pokemonName)
        {
            try
            {
                return await _context.CapturedPokemons.FirstOrDefaultAsync(x =>
                        x.PokemonName == pokemonName
                    ) != null;
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
                                capturedPokemon.PokemonUrl,
                            }
                    )
                    .Select(x => new CapturedPokemonsDTO
                    {
                        PokemonName = x.PokemonName,
                        PokemonUrl = x.PokemonUrl,
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

        public async Task<IEnumerable<CapturedPokemonsDTO>> GetCapturedPokemonsByUser(long id)
        {
            try
            {
                var captured = await _context
                    .CapturedPokemons.Where(x => x.UserId == id)
                    .Join(
                        _context.Users,
                        users => users.UserId,
                        user => user.Id,
                        (users, user) => new { users, user }
                    )
                    .Select(x => new CapturedPokemonsDTO
                    {
                        PokemonName = x.users.PokemonName,
                        PokemonUrl = x.users.PokemonUrl,
                        CapturedAt = x.users.CapturedAt,
                        User = new UserDTO
                        {
                            Username = x.user.Username,
                            Id = x.user.Id,
                            Name = x.user.Name,
                        },
                    })
                    .ToListAsync();

                return captured;
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
