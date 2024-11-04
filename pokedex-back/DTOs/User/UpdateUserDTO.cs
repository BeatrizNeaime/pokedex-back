using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokedex_back.DTOs.User
{
    public class UpdateUserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Token { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
