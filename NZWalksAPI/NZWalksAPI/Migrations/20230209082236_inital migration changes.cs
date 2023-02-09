using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class initalmigrationchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalkDifficulties_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalkDifficulties",
                table: "WalkDifficulties");

            migrationBuilder.RenameTable(
                name: "WalkDifficulties",
                newName: "WalkDifficulty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalkDifficulty",
                table: "WalkDifficulty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalkDifficulty_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId",
                principalTable: "WalkDifficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalkDifficulty_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalkDifficulty",
                table: "WalkDifficulty");

            migrationBuilder.RenameTable(
                name: "WalkDifficulty",
                newName: "WalkDifficulties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalkDifficulties",
                table: "WalkDifficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalkDifficulties_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId",
                principalTable: "WalkDifficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
