using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TechLibrary_App.DTOs
{
    public class CredencialesUsuario : IdentityUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }        
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        [StringLength(50)]
        public string Direccion { get; set; }
    }
}
