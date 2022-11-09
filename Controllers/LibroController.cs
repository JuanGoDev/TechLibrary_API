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
    [Route("api/libros")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class LibroController : Controller
    {
        private readonly ILogger<LibroController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public LibroController(ILogger<LibroController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Libro>>> Get()
        {
            return await context.Libros.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            Libro libro = await context.Libros.FirstOrDefaultAsync(context => context.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            return mapper.Map<LibroDTO>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LibroCreacionDTO libroCreacionDTO)
        {
            Libro libro = mapper.Map<Libro>(libroCreacionDTO);
            context.Add(libro);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Libro libro)
        {

            if (id != libro.Id)
            {
                return BadRequest("El libro no existe.");
            }

            bool existe = await context.Libros.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();//200
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Libro libro = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            context.Remove(libro);

            await context.SaveChangesAsync();

            return NoContent(); //204
        }
    }
}
