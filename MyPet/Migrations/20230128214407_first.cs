using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPet.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tutores",
                columns: table => new
                {
                    TutorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutores", x => x.TutorId);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Porte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Raca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    HashCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pets_Tutores_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutores",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tutores",
                columns: new[] { "TutorId", "Cep", "Email", "Nome", "Password" },
                values: new object[,]
                {
                    { 1, "45028-125", "andre@gmail.com", "Andre", "123456" },
                    { 2, "45028-674", "luisa@gmail.com", "Luisa", "789654" },
                    { 3, "41250-330", "lara@gmail.com", "Lara", "147852" },
                    { 4, "45028618", "livia@gmail.com", "Livia", "987654" }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "PetId", "HashCode", "Nome", "Porte", "Raca", "TutorId" },
                values: new object[,]
                {
                    { 1, 0, "July", "Pequeno", "Cão", 1 },
                    { 2, 0, "Bob", "Pequeno", "Cachorro", 2 },
                    { 3, 0, "Bisteca", "´Médio", "Cachorro", 3 },
                    { 4, 0, "Alecrim", "Pequeno", "Gato", 4 },
                    { 5, 0, "Mini", "Pequeno", "Gato", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_TutorId",
                table: "Pets",
                column: "TutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Tutores");
        }
    }
}
