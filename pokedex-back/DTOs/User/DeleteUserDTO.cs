using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokedex_back.DTOs.User
{
    public class DeleteUserDTO
    {
        public long Id { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
