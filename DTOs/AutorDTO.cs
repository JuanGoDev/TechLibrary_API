using System.ComponentModel.DataAnnotations;
using TechLibrary_App.Entidades;

namespace TechLibrary_App.DTOs
{
    public class AutorDTO
    {
        public int Id { get; set; }
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
        public string FotoAutor { get; set; }
    }
}
