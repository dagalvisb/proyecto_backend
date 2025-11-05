using Castle.Core.Configuration;
using Usuarios.server.Controllers;
using Usuarios.server.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace TestProject1
{
    public class MateriasControllerTest : TestBase
    { 
        private UsuariosController _controller;


        public MateriasControllerTest()
        {
            _controller = new UsuariosController(context);
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

            var result = await _controller.CrearMateria(newMateria);

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
