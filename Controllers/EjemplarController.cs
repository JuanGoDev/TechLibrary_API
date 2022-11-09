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
    [Route("api/ejemplares")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class EjemplarController : Controller
    {
        private readonly ILogger<EjemplarController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EjemplarController(ILogger<EjemplarController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ejemplar>>> Get()
        {
            return await context.Ejemplares.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EjemplarDTO>> Get(int id)
        {
            Ejemplar ejemplar = await context.Ejemplares.FirstOrDefaultAsync(context => context.Id == id);

            if (ejemplar == null)
            {
                return NotFound();
            }

            return mapper.Map<EjemplarDTO>(ejemplar);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EjemplarCreacionDTO ejemplarCreacionDTO)
        {
            Ejemplar ejemplar = mapper.Map<Ejemplar>(ejemplarCreacionDTO);
            context.Add(ejemplar);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Ejemplar ejemplar)
        {

            if (id != ejemplar.Id)
            {
                return BadRequest("El ejemplar no existe.");
            }

            bool existe = await context.Ejemplares.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(ejemplar);
            await context.SaveChangesAsync();
            return Ok();//200
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Ejemplar ejemplar = await context.Ejemplares.FirstOrDefaultAsync(x => x.Id == id);
            if (ejemplar == null)
            {
                return NotFound();
            }

            context.Remove(ejemplar);

            await context.SaveChangesAsync();

            return NoContent(); //204
        }
    }
}
