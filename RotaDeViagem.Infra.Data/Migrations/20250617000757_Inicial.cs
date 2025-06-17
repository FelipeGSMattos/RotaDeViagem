using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RotaDeViagem.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rotas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Origem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Destino = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rotas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Rotas",
                columns: new[] { "Id", "Destino", "Origem", "Valor" },
                values: new object[,]
                {
                    { new Guid("30977f26-5eef-4fd6-9688-4d8c076f54b9"), "SCL", "BRC", 10m },
                    { new Guid("396822c5-ca79-4ea0-a1a1-a6fb8b54561f"), "BRC", "GRU", 10m },
                    { new Guid("3a50f582-6541-4f81-8117-4d5e1db789be"), "ORL", "SCL", 20m },
                    { new Guid("3a9dacca-b4de-4045-882c-c400caa30be0"), "ORL", "GRU", 56m },
                    { new Guid("a3501db8-cee8-49e8-a9c2-b1141467c057"), "SCL", "GRU", 75m },
                    { new Guid("aac4c8e7-3f93-4686-b35e-948571f9b98d"), "CDG", "GRU", 75m },
                    { new Guid("c3e19a3b-1eed-4fe8-b130-20291131940b"), "CDG", "ORL", 5m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rotas");
        }
    }
}
