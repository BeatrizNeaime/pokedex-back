namespace pokedex_back.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}
