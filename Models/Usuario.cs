using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TechLibrary_App.Entidades;

namespace TechLibrary_App.Models
{
    public class Usuario : IdentityUser
    {                
        [StringLength(50)]
        public string Nombre { get; set; }        
        [StringLength(50)]
        public string Apellido { get; set; }        
        public int Telefono { get; set; }        
        [StringLength(50)]
        public string Direccion { get; set; }
        [JsonIgnore]
        public ICollection<UsuarioEjemplar> UsuarioEjemplares { get; set; }
    }
}
