using System.ComponentModel.DataAnnotations;

namespace TechLibrary_App.DTOs
{
    public class AutorCreacionDTO
    {
        [Required]
        public int Documento { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [StringLength(20)]
        public string Sexo { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(20)]
        public string PaisOrigen { get; set; }
        [Required]
        public IFormFile FotoAutor { get; set; }
    }
}
