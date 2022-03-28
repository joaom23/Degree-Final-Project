using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_CMS_API.Migrations
{
    public partial class addcordefundo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorDeFundo",
                table: "Layout",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorDeFundo",
                table: "Layout");
        }
    }
}
