using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pokedex_back.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("hash")]
        public byte[] Hash { get; set; } = [];

        [Column("salt")]
        public byte[] Salt { get; set; } = [];

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        public void SetPassword(string password)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                Salt = hmac.Key;
                Hash = hmac.ComputeHash(passwordBytes);
            }
        }
    }
}
