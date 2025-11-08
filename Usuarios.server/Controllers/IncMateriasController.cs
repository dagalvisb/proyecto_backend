using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Usuarios.server.Data;
using Usuarios.server.Models;

namespace Usuarios.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncMateriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ApplicationDbContext Context { get; }

        public IncMateriasController(ApplicationDbContext context, IConfiguration configuration) { 

            _context = context;
            _configuration = configuration;

        }

        [HttpGet("Unicas")]
        public async Task<IActionResult> GetMateriasUnicas()
        {
            var materias = new List<string>();

            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    // 🔹 Iniciar transacción
                    using (var transaction = await connection.BeginTransactionAsync())
                    {
                        // 1️⃣ Llamar al procedimiento que abre el cursor
                        using (var callCmd = new NpgsqlCommand("CALL sp_GetNombreMaterias('ref');", connection, transaction))
                        {
                            await callCmd.ExecuteNonQueryAsync();
                        }

                        // 2️⃣ Hacer el FETCH dentro de la misma transacción
                        using (var fetchCmd = new NpgsqlCommand("FETCH ALL IN \"ref\";", connection, transaction))
                        using (var reader = await fetchCmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                materias.Add(reader.GetString(0));
                            }
                        }

                        // 3️⃣ Confirmar la transacción
                        await transaction.CommitAsync();
                    }
                }

                return Ok(materias);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al obtener las materias únicas", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("CrearincMaterias")]
        public async Task<IActionResult> IncMaterias(incMaterias incmaterias)
        {
            await _context.IncMaterias.AddAsync(incmaterias);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Inscripción creada exitosamente" });
        }

        [HttpGet]
        [Route("ListaIncMaterias")]
        public async Task<ActionResult<IEnumerable<incMaterias>>> ListaIncMaterias()
        {
            var incmaterias = await _context.IncMaterias.ToListAsync();
            return Ok(incmaterias);
        }

        [HttpGet]
        [Route("verincMaterias")]
        public async Task<IActionResult> VerIncMaterias(int id)
        {
            incMaterias incmaterias = await _context.IncMaterias.FindAsync(id);
            if (incmaterias == null)
            {
                return NotFound();
            }
            return Ok(incmaterias);
        }

        [HttpPut]
        [Route("EditarIncMaterias")]
        public async Task<IActionResult> EditarIncMaterias(int id, incMaterias incmaterias)
        {
            var incmateriasExistente = await _context.IncMaterias.FindAsync(id);
            incmateriasExistente.usuario = incmaterias.usuario;
            incmateriasExistente.materia = incmaterias.materia;
            incmateriasExistente.createdDate = incmaterias.createdDate;
            await _context.SaveChangesAsync();
            return Ok(incmateriasExistente);
        }

        [HttpDelete]
        [Route("EliminarIncMaterias")]
        public async Task<IActionResult> EliminarIncMaterias(int id)
        {
            var incmateriasBorrada = await _context.IncMaterias.FindAsync(id);
            _context.IncMaterias.Remove(incmateriasBorrada!);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
