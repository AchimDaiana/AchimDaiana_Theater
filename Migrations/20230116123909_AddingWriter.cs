using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AchimDaianaTheater.Migrations
{
    /// <inheritdoc />
    public partial class AddingWriter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Writer",
                table: "Play");

            migrationBuilder.AddColumn<int>(
                name: "WriterID",
                table: "Play",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Writer",
                columns: table => new
                {
                    WriterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writer", x => x.WriterID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Play_WriterID",
                table: "Play",
                column: "WriterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Play_Writer_WriterID",
                table: "Play",
                column: "WriterID",
                principalTable: "Writer",
                principalColumn: "WriterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Play_Writer_WriterID",
                table: "Play");

            migrationBuilder.DropTable(
                name: "Writer");

            migrationBuilder.DropIndex(
                name: "IX_Play_WriterID",
                table: "Play");

            migrationBuilder.DropColumn(
                name: "WriterID",
                table: "Play");

            migrationBuilder.AddColumn<string>(
                name: "Writer",
                table: "Play",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
