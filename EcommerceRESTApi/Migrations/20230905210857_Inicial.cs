using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceRESTApi.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryTypes_CategoryTypeId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryTypeId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryTypeId",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryTypeId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryTypeId",
                table: "Categories",
                column: "CategoryTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryTypes_CategoryTypeId",
                table: "Categories",
                column: "CategoryTypeId",
                principalTable: "CategoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
