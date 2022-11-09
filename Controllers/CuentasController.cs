namespace TechLibrary_App.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using TechLibrary_App.Data;
    using TechLibrary_App.DTOs;
    using TechLibrary_App.Models;
       
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<Usuario> userManager;

        private readonly IConfiguration configuration;
        private readonly SignInManager<Usuario> signInManager;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CuentasController> logger;

        public CuentasController(UserManager<Usuario> userManager, IConfiguration configuration, SignInManager<Usuario> signInManager, ApplicationDbContext context, IMapper mapper, ILogger<CuentasController> logger)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            Usuario usuario = new Usuario
            {
                UserName = credencialesUsuario.Email,
                Email = credencialesUsuario.Email,
                Nombre = credencialesUsuario.Nombre,
                Apellido = credencialesUsuario.Apellido,
                Telefono = credencialesUsuario.Telefono,
                Direccion = credencialesUsuario.Direccion
            };

            IdentityResult resultado = await userManager.CreateAsync(usuario, credencialesUsuario.Password);

            if (resultado.Succeeded)
            {    
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }

        }

        [HttpPost("login")] // Inicio de sesión
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {

            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
                credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }

        }

        [HttpPost("AsignarRol")] // Asignar Claim/Rol
        public async Task<ActionResult> AddClaimsToUser(string email, string claimname, string claimValue)
        {

            //Validar si el usario existe
            Usuario user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                logger.LogInformation($"El usuario elegido con el email: {email}, no existe");
                return BadRequest(
                    new
                    {
                        error = "El usario no existe"

                    });
            }

            var userClaim = new Claim(claimname, claimValue);
            var result = await userManager.AddClaimAsync(user, userClaim);

            if (result.Succeeded)
            {
                return Ok(
                    new
                    {
                        result = $"Al usuario {user.Email}, se le asignó el claim de: {claimname} "

                    });
            }

            return BadRequest(
                new
                {
                    error = $"No fue posible asignarle el Claim: {claimname} al usuario {user.Email}"
                });
        }

        private async Task<ActionResult<RespuestaAutenticacion>> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuario.Email),
                new Claim("lo que sea", "cualquier valor")
            };

            Usuario usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            IList<Claim> claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            SymmetricSecurityKey llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            SigningCredentials creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            DateTime expiracion = DateTime.UtcNow.AddDays(30);

            JwtSecurityToken securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }
    }
}