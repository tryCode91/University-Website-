using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teaching.Migrations
{
    /// <inheritdoc />
    public partial class changeSMModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedia_Student_StudentId",
                table: "SocialMedia");

            migrationBuilder.DropIndex(
                name: "IX_SocialMedia_StudentId",
                table: "SocialMedia");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "SocialMedia");

            migrationBuilder.AddColumn<int>(
                name: "SocialMediaId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_SocialMediaId",
                table: "Student",
                column: "SocialMediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SocialMedia_SocialMediaId",
                table: "Student",
                column: "SocialMediaId",
                principalTable: "SocialMedia",
                principalColumn: "SocialMediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_SocialMedia_SocialMediaId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_SocialMediaId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "SocialMediaId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "SocialMedia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_StudentId",
                table: "SocialMedia",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedia_Student_StudentId",
                table: "SocialMedia",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentId");
        }
    }
}
