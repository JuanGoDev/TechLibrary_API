using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechLibrary_App.Data;
using TechLibrary_App.DTOs;
using TechLibrary_App.Entidades;
using TechLibrary_App.Servicios;

namespace TechLibrary_App.Controllers
{
    [ApiController]
    [Route("api/autores")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class AutorController : Controller
    {
        private readonly ILogger<AutorController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "Files";

        public AutorController(ILogger<AutorController> logger, ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            Autor autor = await context.Autores.FirstOrDefaultAsync(context => context.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return mapper.Map<AutorDTO>(autor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] AutorCreacionDTO autorCreacionDTO)
        {
            Autor imagenVehiculo = mapper.Map<Autor>(autorCreacionDTO);

            if (autorCreacionDTO.FotoAutor != null)
            {
                imagenVehiculo.FotoAutor = await almacenadorArchivos.GuardarArchivo(contenedor, autorCreacionDTO.FotoAutor);
            }

            context.Add(imagenVehiculo);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Autor autor)
        {

            if (id != autor.Id)
            {
                return BadRequest("El autor no existe.");
            }

            bool existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();//200
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Autor autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            context.Remove(autor);

            await context.SaveChangesAsync();

            return NoContent(); //204
        }

    }
}
