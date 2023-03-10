using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyJournal.Data.Migrations
{
    /// <inheritdoc />
    public partial class multipleAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MultipleAuthors",
                table: "Authors",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MultipleAuthors",
                table: "Authors");
        }
    }
}
