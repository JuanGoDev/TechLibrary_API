using System.ComponentModel.DataAnnotations;

namespace TechLibrary_App.DTOs
{
    public class LibroCreacionDTO
    {        
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
