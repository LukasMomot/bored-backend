using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoredBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveFlagOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Bored",
                table: "Offers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5L,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6L,
                column: "IsActive",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Bored",
                table: "Offers");
        }
    }
}
