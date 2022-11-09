using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechLibrary_App.Entidades
{
    public class Libro
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
        [JsonIgnore]
        public ICollection<AutorLibro> AutoresLibros { get; set; }
        [JsonIgnore]
        public ICollection<Ejemplar> Ejemplares { get; set; }
    }
}
