using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechLibrary_App.Entidades
{
    public class AutorLibro
    {
        public int Id { get; set; }
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
