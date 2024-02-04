using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_AspNetUsers_UserId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "Amenities",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Property",
                newName: "AddedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Property_UserId",
                table: "Property",
                newName: "IX_Property_AddedBy");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Property",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AspNetUsers_AddedBy",
                table: "Property",
                column: "AddedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_AspNetUsers_AddedBy",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "AddedBy",
                table: "Property",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_AddedBy",
                table: "Property",
                newName: "IX_Property_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Amenities",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AspNetUsers_UserId",
                table: "Property",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
