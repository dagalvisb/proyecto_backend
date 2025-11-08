using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usuarios.server.Data;
using Usuarios.server.Models;

namespace Usuarios.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContext Context { get; }

        public MateriasController(ApplicationDbContext context) { 
            
            _context = context;

        }

        [HttpPost]
        [Route("crearMateria")]
        public async Task<IActionResult> CrearMateria(Materia materia)
        {
            await _context.Materias.AddAsync(materia);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Materia creada exitosamente" });
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

            return Ok(materiaExistente);
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
    }
}
