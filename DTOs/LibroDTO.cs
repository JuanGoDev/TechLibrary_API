using System.ComponentModel.DataAnnotations;
using TechLibrary_App.Entidades;

namespace TechLibrary_App.DTOs
{
    public class LibroDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        [Required]
        public int ISBN { get; set; }
        [Required]
        [StringLength(50)]
        public string Editorial { get; set; }
        [Required]
        public int Paginas { get; set; }
    }
}
