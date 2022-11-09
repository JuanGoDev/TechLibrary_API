namespace TechLibrary_App.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TechLibrary_App.Data;
    using TechLibrary_App.DTOs;
    using TechLibrary_App.Entidades;

    [ApiController]
    [Route("api/usuariosEjemplares")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class UsuarioEjemplarController : Controller
    {
        private readonly ILogger<UsuarioEjemplarController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuarioEjemplarController(ILogger<UsuarioEjemplarController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioEjemplar>>> Get()
        {
            return await context.UsuariosEjemplares.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioEjemplarDTO>> Get(int id)
        {
            UsuarioEjemplar usuarioEjemplar = await context.UsuariosEjemplares.FirstOrDefaultAsync(context => context.Id == id);

            if (usuarioEjemplar == null)
            {
                return NotFound();
            }

            return mapper.Map<UsuarioEjemplarDTO>(usuarioEjemplar);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioEjemplarCreacionDTO usuarioEjemplarCreacionDTO)
        {
            UsuarioEjemplar usuarioEjemplar = mapper.Map<UsuarioEjemplar>(usuarioEjemplarCreacionDTO);
            context.Add(usuarioEjemplar);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UsuarioEjemplar usuarioEjemplar)
        {

            if (id != usuarioEjemplar.Id)
            {
                return BadRequest("El usuario ó el ejemplar no existe.");
            }

            bool existe = await context.UsuariosEjemplares.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(usuarioEjemplar);
            await context.SaveChangesAsync();
            return Ok();//200
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            UsuarioEjemplar usuarioEjemplar = await context.UsuariosEjemplares.FirstOrDefaultAsync(x => x.Id == id);
            if (usuarioEjemplar == null)
            {
                return NotFound();
            }

            context.Remove(usuarioEjemplar);

            await context.SaveChangesAsync();

            return NoContent(); //204
        }
    }
}
