using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuarios.server.Migrations
{
    /// <inheritdoc />
    public partial class adddbcont : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = File.ReadAllText("Database/StoredProcedures/sp_GetNombreMaterias.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
