using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teaching.Migrations
{
    /// <inheritdoc />
    public partial class sm : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "SocialMediaId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "SocialMedia",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "SocialMedia",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_StudentId",
                table: "SocialMedia",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedia_Student_StudentId",
                table: "SocialMedia",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
