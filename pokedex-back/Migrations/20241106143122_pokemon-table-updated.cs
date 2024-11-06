using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokedex_back.Migrations
{
    /// <inheritdoc />
    public partial class pokemontableupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pokemonUrl",
                table: "capturedPokemons",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pokemonUrl",
                table: "capturedPokemons");
        }
    }
}
