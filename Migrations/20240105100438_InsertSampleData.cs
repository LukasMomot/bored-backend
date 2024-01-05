using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BoredBackend.Migrations
{
    /// <inheritdoc />
    public partial class InsertSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Bored",
                table: "Activities",
                columns: new[] { "Id", "Accessibility", "ExternalKey", "Link", "Name", "Participants", "Price", "Type" },
                values: new object[,]
                {
                    { 1, 0.35m, "9072906", "", "Go to a karaoke bar with some friends", 4, 0.5m, "social" },
                    { 2, 0.4m, "1093640", "", "Play a game of tennis with a friend", 2, 0.1m, "social" },
                    { 3, 0.5m, "1382389", "", "Volunteer at a local animal shelter", 1, 0.1m, "charity" }
                });

            migrationBuilder.InsertData(
                schema: "Bored",
                table: "Offers",
                columns: new[] { "Id", "ActivityId", "BuyUrl", "Name" },
                values: new object[,]
                {
                    { 1L, 1, "https://www.karaokebar.com", "Karaoke Bar" },
                    { 2L, 1, "https://www.karaokebar2.com", "Karaoke Bar 2" },
                    { 3L, 2, "https://www.tennisclub.com", "Tennis Club" },
                    { 4L, 2, "https://www.tennisclub2.com", "Tennis Club 2" },
                    { 5L, 3, "https://www.animalshelter.com", "Animal Shelter" },
                    { 6L, 3, "https://www.animalshelter2.com", "Animal Shelter 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Offers",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Bored",
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
