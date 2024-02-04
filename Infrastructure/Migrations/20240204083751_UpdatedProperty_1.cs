using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProperty_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Property_PropertyListingId",
                table: "ContactDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_AspNetUsers_AddedBy",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_AddedBy",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_PropertyListingId",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Bathrooms",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "PropertyListingId",
                table: "ContactDetails");

            migrationBuilder.RenameColumn(
                name: "Bedrooms",
                table: "Property",
                newName: "ContactDetailsId");

            migrationBuilder.AlterColumn<string>(
                name: "AddedBy",
                table: "Property",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Property",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Property_ContactDetailsId",
                table: "Property",
                column: "ContactDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Property_UserId",
                table: "Property",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AspNetUsers_UserId",
                table: "Property",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Property_ContactDetails_ContactDetailsId",
                table: "Property",
                column: "ContactDetailsId",
                principalTable: "ContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Property_AspNetUsers_UserId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_ContactDetails_ContactDetailsId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_ContactDetailsId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_UserId",
                table: "Property");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "ContactDetailsId",
                table: "Property",
                newName: "Bedrooms");

            migrationBuilder.AlterColumn<string>(
                name: "AddedBy",
                table: "Property",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Bathrooms",
                table: "Property",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PropertyListingId",
                table: "ContactDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Property_AddedBy",
                table: "Property",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_PropertyListingId",
                table: "ContactDetails",
                column: "PropertyListingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Property_PropertyListingId",
                table: "ContactDetails",
                column: "PropertyListingId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_AspNetUsers_AddedBy",
                table: "Property",
                column: "AddedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
