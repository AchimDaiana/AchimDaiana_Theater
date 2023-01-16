using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AchimDaianaTheater.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Play",
                type: "decimal(6,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "Theater",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheaterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theater", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TheaterPlay",
                columns: table => new
                {
                    TheaterID = table.Column<int>(type: "int", nullable: false),
                    PlayID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheaterPlay", x => new { x.PlayID, x.TheaterID });
                    table.ForeignKey(
                        name: "FK_TheaterPlay_Play_PlayID",
                        column: x => x.PlayID,
                        principalTable: "Play",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TheaterPlay_Theater_TheaterID",
                        column: x => x.TheaterID,
                        principalTable: "Theater",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TheaterPlay_TheaterID",
                table: "TheaterPlay",
                column: "TheaterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TheaterPlay");

            migrationBuilder.DropTable(
                name: "Theater");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Play",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)");
        }
    }
}
