using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShareHub.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookGenreUpdate_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookGenre",
                table: "Books",
                newName: "Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Books",
                newName: "BookGenre");
        }
    }
}
