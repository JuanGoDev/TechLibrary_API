using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TechLibrary_App.Models;

namespace TechLibrary_App.Entidades
{
    public class UsuarioEjemplar
    {
        public int Id { get; set; }
        [Required]
        public string IdUsuario { get; set; }
        [Required]
        public int IdEjemplar { get; set; }
        [Required]
        public DateTime FechaPrestamo { get; set; }
        [Required]
        public DateTime FechaDevolucion { get; set; }
        [JsonIgnore]
        public Usuario Usuarios { get; set; }
        [JsonIgnore]
        public Ejemplar Ejemplares { get; set; }
    }
}
