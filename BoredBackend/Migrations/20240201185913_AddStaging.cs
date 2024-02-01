using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoredBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddStaging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityStaging",
                schema: "Bored",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Participants = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Accessibility = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStaging", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityStaging",
                schema: "Bored");
        }
    }
}
