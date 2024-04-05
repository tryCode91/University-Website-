using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teaching.Migrations
{
    /// <inheritdoc />
    public partial class smallchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProfileImage");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ProfileImage",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
