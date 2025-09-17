using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Usuarios.server.Models;

namespace Usuarios.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioContext _context;
        public UsuariosController(UsuarioContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<IActionResult>CrearUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("ListaUsuario")]
        public async Task<ActionResult<IEnumerable<Usuario>>>ListaProductos()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet]
        [Route("verUsuario")]
        public async Task<IActionResult>VerUsuario(int id)
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

        public async Task<IActionResult>EditarUsuario(int id, Usuario usuario)
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
            usuarioExistente.Fecha = usuario.Fecha;
            usuarioExistente.firma = usuario.firma;
            usuarioExistente.bloque1 = usuario.bloque1;
            usuarioExistente.bloque2 = usuario.bloque2;

            await _context.SaveChangesAsync();

            return Ok(); 
        }

        [HttpDelete]
        [Route("EliminarUsuario")]
        public async Task<IActionResult>EliminarUsuario(int id)
        {
            var usuarioBorrado = await _context.Usuarios.FindAsync(id);
            
            _context.Usuarios.Remove(usuarioBorrado!);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("crearMateria")]
        public async Task<IActionResult> CrearMateria(Materia materia)
        {
            await _context.Materias.AddAsync(materia);
            await _context.SaveChangesAsync();
            return Ok();
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
            materiaExistente.nombre = materia.nombre;
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
    }
}
