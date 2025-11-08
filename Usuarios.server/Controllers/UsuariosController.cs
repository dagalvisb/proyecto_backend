using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections;
using Usuarios.server.Data;
using Usuarios.server.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Usuarios.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
   
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContext Context { get; }

        public UsuariosController(ApplicationDbContext context, IConfiguration configuration)
        {
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
        [Route("CrearUsuario")]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Usuario creado exitosamente" });
        }

        [HttpGet]
        [Route("ListaUsuario")]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListaProductos()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("verUsuario")]
        public async Task<IActionResult> VerUsuario(int id)
        {
            Usuario usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPut]
        [Route("EditarUsuario")]

        public async Task<IActionResult> EditarUsuario(int id, Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            usuarioExistente.nombre = usuario.nombre;
            usuarioExistente.lugarNacimiento = usuario.lugarNacimiento;
            usuarioExistente.dni = usuario.dni;
            usuarioExistente.correo = usuario.correo;
            usuarioExistente.direccion = usuario.direccion;
            usuarioExistente.cp = usuario.cp;
            usuarioExistente.ciudad = usuario.ciudad;
            usuarioExistente.movil = usuario.movil;
            usuarioExistente.firma = usuario.firma;
            usuarioExistente.tipo_usuario = usuario.tipo_usuario;
            usuarioExistente.bloque1 = usuario.bloque1;
            usuarioExistente.bloque2 = usuario.bloque2;

            await _context.SaveChangesAsync();

            return Ok(usuarioExistente);
        }

        [HttpDelete]
        [Route("EliminarUsuario")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuarioBorrado = await _context.Usuarios.FindAsync(id);

            _context.Usuarios.Remove(usuarioBorrado!);

            await _context.SaveChangesAsync();

            return Ok();
        }
        

        [HttpPost]
        [Route("crearMateria")]
        public async Task<IActionResult>CrearMateria(Materia materia)
        {
            await _context.Materias.AddAsync(materia);
            await _context.SaveChangesAsync();
            return Ok (new { message = "Materia creada exitosamente" });
        }

        [HttpGet]
        [Route("ListaMaterias")]
        public async Task<ActionResult<IEnumerable<Materia>>> ListaMaterias()
        {
            var materias = await _context.Materias.ToListAsync();
            return Ok(materias);
        }

        [HttpGet]
        [Route("verMaterias")]
        public async Task<IActionResult> VerMaterias(int id)
        {
            Materia materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }
            return Ok(materia);
        }

        [HttpPut]
        [Route("EditarMateria")]
        public async Task<IActionResult> EditarMateria(int id, Materia materia)
        {
            var materiaExistente = await _context.Materias.FindAsync(id);
            materiaExistente.codigo = materia.codigo;
            materiaExistente.materia = materia.materia;
            materiaExistente.semestre = materia.semestre;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("EliminarMateria")]
        public async Task<IActionResult> EliminarMateria(int id)
        {
            var materiaBorrada = await _context.Materias.FindAsync(id);

            _context.Materias.Remove(materiaBorrada!);

            await _context.SaveChangesAsync();

            return Ok();
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