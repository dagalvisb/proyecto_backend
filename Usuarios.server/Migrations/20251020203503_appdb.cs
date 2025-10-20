using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuarios.server.Migrations
{
    /// <inheritdoc />
    public partial class appdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_dni",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materias",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_Materias_codigo",
                table: "Materias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_incMaterias",
                table: "incMaterias");

            migrationBuilder.DropIndex(
                name: "IX_incMaterias_usuario",
                table: "incMaterias");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "usuarios");

            migrationBuilder.RenameTable(
                name: "Materias",
                newName: "materias");

            migrationBuilder.RenameTable(
                name: "incMaterias",
                newName: "incmaterias");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "usuarios",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "materias",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "incmaterias",
                newName: "id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedDate",
                table: "usuarios",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdDate",
                table: "usuarios",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedDate",
                table: "materias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdDate",
                table: "materias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdDate",
                table: "incmaterias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_materias",
                table: "materias",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_incmaterias",
                table: "incmaterias",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_materias",
                table: "materias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_incmaterias",
                table: "incmaterias");

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "materias",
                newName: "Materias");

            migrationBuilder.RenameTable(
                name: "incmaterias",
                newName: "incMaterias");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Materias",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "incMaterias",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedDate",
                table: "Usuarios",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdDate",
                table: "Usuarios",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updatedDate",
                table: "Materias",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdDate",
                table: "Materias",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdDate",
                table: "incMaterias",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materias",
                table: "Materias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_incMaterias",
                table: "incMaterias",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_dni",
                table: "Usuarios",
                column: "dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materias_codigo",
                table: "Materias",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_incMaterias_usuario",
                table: "incMaterias",
                column: "usuario");
        }
    }
}
