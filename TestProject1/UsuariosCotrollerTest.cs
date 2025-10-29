using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Usuarios.server.Controllers;
using Usuarios.server.Data;
using Usuarios.server.Models;
using Xunit;

namespace TestProyect1
{
    public class UsuariosControllerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly UsuariosController _controller;

        public UsuariosControllerTests()
        {
            // Configurar DbContext en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nombre único para cada test
                .Options;

            _context = new ApplicationDbContext(options);
            _mockConfiguration = new Mock<IConfiguration>();
            _controller = new UsuariosController(_context, _mockConfiguration.Object);

            // Limpiar y inicializar la base de datos en memoria
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        private Usuario CrearUsuarioValido(int id = 0)
        {
            var usuario = new Usuario
            {
                nombre = "Daniel Galvis",
                correo = "vdagalvis@gmail.com",
                lugarNacimiento = "Manizales",
                dni = "1053814829",
                direccion = "Vereda la Aurora",
                cp = "17002",
                ciudad = "Manizales",
                movil = "3193949304",
                firma = "Daniel",
                tipo_usuario = "Estudiante",
                bloque1 = "",
                bloque2 = ""
            };

            if (id > 0)
            {
                usuario.Id = id;
            }

            return usuario;
        }

        [Fact]
        public async Task TestCrearUsuario()
        {
            // Arrange
            var usuario = CrearUsuarioValido();

            // Act
            var result = await _controller.CrearUsuario(usuario);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            // Usar reflexión para acceder a la propiedad del objeto anónimo
            var messageProperty = okResult.Value.GetType().GetProperty("message");
            Assert.NotNull(messageProperty); // Verificar que la propiedad existe

            var messageValue = messageProperty.GetValue(okResult.Value) as string;
            Assert.NotNull(messageValue);
            Assert.Equal("Usuario creado exitosamente", messageValue);

            // Verificar que se guardó en la base de datos
            var usuarioEnDb = await _context.Usuarios.FirstOrDefaultAsync();
            Assert.NotNull(usuarioEnDb);
            Assert.Equal("Daniel Galvis", usuarioEnDb.nombre);
            Assert.Equal("1053814829", usuarioEnDb.dni);
        }

        [Fact]
        public async Task TestListaUsuario()
        {
            // Arrange - Usar el método helper para crear usuarios válidos
            var usuarios = new List<Usuario>
            {
                CrearUsuarioValido(1),
                CrearUsuarioValido(2)
            };

            await _context.Usuarios.AddRangeAsync(usuarios);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.ListaProductos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Usuario>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task TestVerUsuario()
        {
            // Arrange
            var usuario = CrearUsuarioValido(1);
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.VerUsuario(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Usuario>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Daniel Galvis", returnValue.nombre);
        }

        [Fact]
        public async Task TestEditarUsuario()
        {
            // Arrange
            var usuarioExistente = new Usuario
            {
                Id = 1,
                nombre = "Daniel Galvis",
                correo = "vdagalvis@gmail.com",
                lugarNacimiento = "Manizales",
                dni = "1053814829",
                direccion = "Vereda la Aurora",
                cp = "17002",
                ciudad = "Manizales",
                movil = "3193949304",
                firma = "Daniel",
                tipo_usuario = "Estudiante",
                bloque1 = "",
                bloque2 = ""
            };

            await _context.Usuarios.AddAsync(usuarioExistente);
            await _context.SaveChangesAsync();

            var usuarioActualizado = new Usuario
            {
                nombre = "Daniel Galvis Betancur",
                correo = "dagalvis@gmail.com",
                lugarNacimiento = "Manizales",
                dni = "1053814829",
                direccion = "Vereda la Aurora",
                cp = "17002",
                ciudad = "Manizales",
                movil = "3193949304",
                firma = "Daniel",
                tipo_usuario = "Estudiante",
                bloque1 = "",
                bloque2 = ""
            };

            // Act
            var result = await _controller.EditarUsuario(1, usuarioActualizado);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Usuario>(okResult.Value);

            Assert.Equal("Daniel Galvis Betancur", returnValue.nombre);
            Assert.Equal("dagalvis@gmail.com", returnValue.correo);
            Assert.Equal("Manizales", returnValue.ciudad);
            Assert.Equal("1053814829", returnValue.dni);

            // Verificar cambios en la base de datos
            var usuarioEnDb = await _context.Usuarios.FindAsync(1);
            Assert.Equal("Daniel Galvis Betancur", usuarioEnDb.nombre);
            Assert.Equal("dagalvis@gmail.com", usuarioEnDb.correo);
        }

        [Fact]
        public async Task TestEliminarUsuario()
        {
            // Arrange
            var usuario = CrearUsuarioValido(1);
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Verificar que existe antes de eliminar
            var usuarioAntes = await _context.Usuarios.FindAsync(1);
            Assert.NotNull(usuarioAntes);

            // Act
            var result = await _controller.EliminarUsuario(1);

            // Assert
            Assert.IsType<OkResult>(result);

            // Verificar que fue eliminado
            var usuarioDespues = await _context.Usuarios.FindAsync(1);
            Assert.Null(usuarioDespues);
        }

    }
}
