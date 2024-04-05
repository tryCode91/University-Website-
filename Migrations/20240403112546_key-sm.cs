using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teaching.Migrations
{
    /// <inheritdoc />
    public partial class keysm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_SocialMediaId",
                table: "Student");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SocialMediaId",
                table: "Student",
                column: "SocialMediaId",
                unique: true,
                filter: "[SocialMediaId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_SocialMediaId",
                table: "Student");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SocialMediaId",
                table: "Student",
                column: "SocialMediaId");
        }
    }
}
