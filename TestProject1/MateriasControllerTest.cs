using Usuarios.server.Controllers;
using Usuarios.server.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace TestProject1
{
    public class MateriasControllerTest : TestBase
    {
        private UsuariosController _controller;

        public MateriasControllerTest()
        {
            // Necesitas mockear IConfiguration
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            _controller = new UsuariosController(context, configuration);
        }

        [Fact]
        public async Task CrearMateria_DeberiaCrearMateriaCorrectamente()
        {
            // Arrange
            var newMateria = new Materia
            {
                codigo = "MAT105",
                materia = "Matematicas Discretas",
                semestre = 6
            };

            // Act
            var result = await _controller.CrearMateria(newMateria);

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
            var materiaEnDb = context.Materias.FirstOrDefault(m => m.codigo == "MAT105");
            materiaEnDb.Should().NotBeNull();
            materiaEnDb.materia.Should().Be("Matematicas Discretas");
            materiaEnDb.semestre.Should().Be(6);
        }

        [Fact]
        public async Task PostUsuario_CrearUsuario()
        {
            var newMateria = new Materia
            {
                codigo = "MAT105",
                materia = "Matematicas Discretas",
                semestre = 6
            };

            var result = await _controller.CrearMaterias(newMateria);

            result.Should().NotBeNull();
            result.Result.Should().NotBeNull();

            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createResult = result.Result as CreatedAtActionResult;
            var materia = createResult?.Value as Materia;
            materia.Should().NotBeNull();
            materia!.codigo.Should().Be("MAT105");
            materia.createdDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            
        }
    }
}
 
