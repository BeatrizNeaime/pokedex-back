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

                if (await GetCapturedPokemonsByUser(user.Id) == 3)
                {
                    throw new Exception("User already has 3 pokemons");
                }

                var capturedPokemon = new CapturedPokemon
                {
                    UserId = user.Id,
                    PokemonName = capture.PokemonName,
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
                    await _context.CapturedPokemons.FirstOrDefaultAsync(x => x.PokemonName == release.PokemonName && x.UserId == release.UserId)
                    ?? throw new Exception("Pokemon not found");

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
