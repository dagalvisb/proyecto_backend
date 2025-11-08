using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Usuarios.server.Controllers;
using Usuarios.server.Data;
using Usuarios.server.Models;
using Xunit;

namespace TestProject1
{
    public class MateriasControllerTest : TestBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MateriasController _controller;

        public MateriasControllerTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Nombre único para cada test
                .Options;


            _context = new ApplicationDbContext(options);
            _controller = new MateriasController(_context);

            // Limpiar y inicializar la base de datos en memoria
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

        }

        private Materia CrearMateriaValida(int id = 0)
        {
            var materia = new Materia
            {
                codigo = "MAT105",
                materia = "Matematicas Discretas",
                semestre = 6
            };

            if (id > 0)
            {
                materia.Id = id;
            }

            return materia;
        }

        [Fact]
        public async Task TestCrearMateria()
        {
            // Arrange
            var materia = CrearMateriaValida();

            // Act
            var result = await _controller.CrearMateria(materia);

            // Assert
            result.Should().NotBeNull();

            // Como tu método retorna Ok(), verifica que sea OkObjectResult
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            // Verifica el mensaje de respuesta
            var response = okResult.Value as dynamic;
            string message = response?.GetType().GetProperty("message")?.GetValue(response, null) as string;
            message.Should().Be("Materia creada exitosamente");

            // Verifica que se guardó en la base de datos
            var materiaEnDb = _context.Materias.FirstOrDefault(m => m.codigo == "MAT105");
            materiaEnDb.Should().NotBeNull();
            materiaEnDb.materia.Should().Be("Matematicas Discretas");
            materiaEnDb.semestre.Should().Be(6);
        }

        [Fact]
        public async Task TestListaMaterias()
        {
            // Arrange - Usar el método helper para crear usuarios válidos
            var materia = new List<Materia>
            {
                CrearMateriaValida(1),
                CrearMateriaValida(2)
            };

            await _context.Materias.AddRangeAsync(materia);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.ListaMaterias();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Materia>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task TestVerMateria()
        {
            // Arrange
            var materia = CrearMateriaValida(1);
            await _context.Materias.AddAsync(materia);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.VerMaterias(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Materia>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("MAT105", returnValue.codigo);
        }

        [Fact]
        public async Task TestEditarUsuario()
        {
            // Arrange
            var materiaExistente = new Materia
            {
                Id = 1,
                codigo = "MAT105",
                materia = "Matematicas Discretas",
                semestre = 6
            };

            await _context.Materias.AddAsync(materiaExistente);
            await _context.SaveChangesAsync();

            var materiaActualizada = new Materia
            {
                codigo = "MAT106",
                materia = "Matematicas Discretas 2",
                semestre = 7
            };

            // Act
            var result = await _controller.EditarMateria(1, materiaActualizada);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Materia>(okResult.Value);

            Assert.Equal("MAT106", returnValue.codigo);
            Assert.Equal("Matematicas Discretas 2", returnValue.materia);

            // Verificar cambios en la base de datos
            var materiaEnDb = await _context.Materias.FindAsync(1);
            Assert.Equal("MAT106", materiaEnDb.codigo);
            Assert.Equal("Matematicas Discretas 2", materiaEnDb.materia);
        }
    }
}
 
