using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Company.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Authors");
        }
    }
}
