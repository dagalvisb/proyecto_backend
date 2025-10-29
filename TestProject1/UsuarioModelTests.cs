using System.ComponentModel.DataAnnotations;
using Usuarios.server.Models;
using Xunit;

namespace TestProyect1
{
    public class UsuarioModelTests
    {
        [Fact]
        public void Usuario_Nombre_ExceedsMaxLength_ReturnsValidationError()
        {
            // Arrange
            var usuario = new Usuario
            {
                nombre = new string('A', 51), // 51 caracteres - excede el máximo
                dni = "12345678A"
            };

            var context = new ValidationContext(usuario, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(usuario, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("nombre"));
        }

        [Theory]
        [InlineData("nombre", 50)]
        [InlineData("dni", 50)]
        [InlineData("correo", 50)]
        [InlineData("bloque1", 500)]
        [InlineData("bloque2", 500)]
        public void Usuario_Property_WithinMaxLength_IsValid(string propertyName, int maxLength)
        {
            // Arrange
            var usuario = new Usuario();
            var property = typeof(Usuario).GetProperty(propertyName);

            // Crear string con longitud exacta al máximo permitido
            var validValue = new string('A', maxLength);
            property.SetValue(usuario, validValue);

            var context = new ValidationContext(usuario, null, null);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(usuario, context, results, true);

            // Assert
            Assert.True(isValid);
        }
    }
}
