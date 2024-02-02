using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_Accounts_AccountId",
                table: "Party");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Party");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Party",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Party_AccountId",
                table: "Party",
                newName: "IX_Party_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Accounts_CreatedById",
                table: "Party",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Party_Accounts_CreatedById",
                table: "Party");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Party",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Party_CreatedById",
                table: "Party",
                newName: "IX_Party_AccountId");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Party",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Party_Accounts_AccountId",
                table: "Party",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
