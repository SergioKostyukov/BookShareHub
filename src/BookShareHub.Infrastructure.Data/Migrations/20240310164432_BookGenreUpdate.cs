using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShareHub.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookGenreUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookGenre",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookGenre",
                table: "Books");
        }
    }
}
