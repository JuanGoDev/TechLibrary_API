using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TechLibrary_App.Entidades;

namespace TechLibrary_App.DTOs
{
    public class AutorLibroCreacionDTO
    {
        [Required]
        public int IdLibro { get; set; }
        [Required]
        public int IdAutor { get; set; }
        [Required]
        public DateTime FechaPublicacion { get; set; }
        [JsonIgnore]
        public Autor Autor { get; set; }
        [JsonIgnore]
        public Libro Libro { get; set; }
    }
}
