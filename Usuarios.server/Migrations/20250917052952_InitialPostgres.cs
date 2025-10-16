using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Usuarios.server.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    lugarNacimiento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    dni = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    correo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    direccion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ciudad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    movil = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    //Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    firma = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    bloque1 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    bloque2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_dni",
                table: "Usuarios",
                column: "dni",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
