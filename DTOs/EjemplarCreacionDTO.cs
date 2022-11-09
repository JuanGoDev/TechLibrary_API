using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TechLibrary_App.Entidades;

namespace TechLibrary_App.DTOs
{
    public class EjemplarCreacionDTO
    {
        [Required]
        public int IdLibro { get; set; }
        [Required]
        [StringLength(50)]
        public string Localizacion { get; set; }
        [JsonIgnore]
        public Libro Libro { get; set; }
    }
}
