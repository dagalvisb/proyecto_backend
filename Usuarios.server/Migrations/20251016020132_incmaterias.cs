using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Usuarios.server.Migrations
{
    /// <inheritdoc />
    public partial class incmaterias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nombre",
                table: "Materias");

            migrationBuilder.CreateTable(
                name: "incMaterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    materia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incMaterias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_incMaterias_usuario",
                table: "incMaterias",
                column: "usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "incMaterias");

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "Materias",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
