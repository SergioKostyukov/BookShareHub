using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShareHub.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "OrdersLists",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Check",
                table: "Orders",
                newName: "CheckAmount");

            migrationBuilder.RenameColumn(
                name: "AddedTime",
                table: "ChatsSubscribersLists",
                newName: "AddedDate");

            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OrdersLists",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CheckAmount",
                table: "Orders",
                newName: "Check");

            migrationBuilder.RenameColumn(
                name: "AddedDate",
                table: "ChatsSubscribersLists",
                newName: "AddedTime");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "AspNetUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
