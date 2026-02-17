using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamaroteFoliaSync.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Camarote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CapacidadeMaxima = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camarote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Folioes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folioes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosFluxo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PulseiraId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosFluxo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosFluxo_DataHora",
                table: "RegistrosFluxo",
                column: "DataHora");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosFluxo_PulseiraId",
                table: "RegistrosFluxo",
                column: "PulseiraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Camarote");

            migrationBuilder.DropTable(
                name: "Folioes");

            migrationBuilder.DropTable(
                name: "RegistrosFluxo");
        }
    }
}
