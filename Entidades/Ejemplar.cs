using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechLibrary_App.Entidades
{
    public class Ejemplar
    {
        public int Id { get; set; }
        [Required]
        public int IdLibro { get; set; }
        [Required]
        [StringLength(50)]
        public string Localizacion { get; set; }
        [JsonIgnore]
        public Libro Libro { get; set; }
        [JsonIgnore]
        public ICollection<UsuarioEjemplar> UsuarioEjemplares { get; set; }
    }
}
