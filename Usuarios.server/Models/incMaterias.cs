using System.ComponentModel.DataAnnotations;

namespace Usuarios.server.Models
{
    public class incMaterias
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string usuario { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener maximo {1} caracteres")]
        public string materia { get; set; } = null!;

        public DateTime createdDate { get; set; }



    }
}
