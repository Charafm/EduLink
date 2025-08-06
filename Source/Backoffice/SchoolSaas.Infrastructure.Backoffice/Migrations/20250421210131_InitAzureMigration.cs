using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSaas.Infrastructure.Backoffice.Migrations
{
    /// <inheritdoc />
    public partial class InitAzureMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "StudentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "StudentDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "StudentDetails");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "StudentDetails");
        }
    }
}
