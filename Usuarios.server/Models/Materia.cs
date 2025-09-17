using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Usuarios.server.Models
{
    public class Materia
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string codigo { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string nombre { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string materia { get; set; } = null!;

        public int semestre { get; set; }
    }
}
