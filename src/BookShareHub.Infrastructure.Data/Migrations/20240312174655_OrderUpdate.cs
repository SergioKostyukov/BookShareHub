using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShareHub.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrdersLists");

            migrationBuilder.RenameColumn(
                name: "OrderType",
                table: "Orders",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "Orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Orders",
                newName: "CloseDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Orders",
                newName: "OrderType");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "CloseDate",
                table: "Orders",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "OrdersLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
