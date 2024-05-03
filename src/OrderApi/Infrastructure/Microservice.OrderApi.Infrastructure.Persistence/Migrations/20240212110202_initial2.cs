using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.OrderApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "Orders",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "OrderStatus");
        }
    }
}
