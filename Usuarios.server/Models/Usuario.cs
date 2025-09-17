using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Usuarios.server.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string nombre { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string lugarNacimiento { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string dni { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string correo { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string direccion { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string cp { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string ciudad { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string movil { get; set; } = null!;

        public DateTime Fecha { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string firma { get; set; } = null!;

        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string bloque1 { get; set; } = "";

        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string bloque2 { get; set; } = "";
    }
}
