using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ksiazka.Data.Migrations
{
    public partial class Baza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Awatar",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data_zalozenia",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nazwa_uzytkownika",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Komentarze",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Komentarz = table.Column<string>(nullable: false),
                    Id_Przepisu = table.Column<int>(nullable: false),
                    Autor = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Awatar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentarze", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Przepis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nazwa = table.Column<string>(nullable: false),
                    Opis = table.Column<string>(nullable: false),
                    Skladniki = table.Column<string>(nullable: false),
                    Czas = table.Column<int>(nullable: false),
                    Trudnosc = table.Column<string>(nullable: true),
                    Kategoria_1 = table.Column<string>(nullable: true),
                    Kategoria_2 = table.Column<string>(nullable: true),
                    Kategoria_3 = table.Column<string>(nullable: true),
                    Zdjecie = table.Column<string>(nullable: true),
                    Autor = table.Column<string>(nullable: true),
                    Data_Dodania = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przepis", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentarze");

            migrationBuilder.DropTable(
                name: "Przepis");

            migrationBuilder.DropColumn(
                name: "Awatar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Data_zalozenia",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nazwa_uzytkownika",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
