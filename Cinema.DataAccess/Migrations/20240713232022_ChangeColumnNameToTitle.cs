using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNameToTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Movies",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Movies",
                newName: "Name");
        }
    }
}
