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
    [Route("api/autoresLibros")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class AutorLibroController : Controller
    {
        private readonly ILogger<AutorLibroController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AutorLibroController(ILogger<AutorLibroController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorLibro>>> Get()
        {
            return await context.AutoresLibros.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorLibroDTO>> Get(int id)
        {
            AutorLibro autorLibro = await context.AutoresLibros.FirstOrDefaultAsync(context => context.Id == id);

            if (autorLibro == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorLibroDTO>(autorLibro);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorLibroCreacionDTO autorLibroCreacionDTO)
        {
            AutorLibro autorLibro = mapper.Map<AutorLibro>(autorLibroCreacionDTO);
            context.Add(autorLibro);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, AutorLibro autorLibro)
        {

            if (id != autorLibro.Id)
            {
                return BadRequest("El autor del libro no existe.");
            }

            bool existe = await context.AutoresLibros.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autorLibro);
            await context.SaveChangesAsync();
            return Ok();//200
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            AutorLibro autorLibro = await context.AutoresLibros.FirstOrDefaultAsync(x => x.Id == id);
            if (autorLibro == null)
            {
                return NotFound();
            }

            context.Remove(autorLibro);

            await context.SaveChangesAsync();

            return NoContent(); //204
        }
    }
}
