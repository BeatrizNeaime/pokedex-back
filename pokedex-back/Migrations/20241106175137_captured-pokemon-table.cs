using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokedex_back.Migrations
{
    /// <inheritdoc />
    public partial class capturedpokemontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_capturedPokemons_userId",
                table: "capturedPokemons",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_capturedPokemons_users_userId",
                table: "capturedPokemons",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_capturedPokemons_users_userId",
                table: "capturedPokemons");

            migrationBuilder.DropIndex(
                name: "IX_capturedPokemons_userId",
                table: "capturedPokemons");
        }
    }
}
