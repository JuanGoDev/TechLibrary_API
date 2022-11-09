using AutoMapper;
using TechLibrary_App.DTOs;
using TechLibrary_App.Entidades;

namespace TechLibrary_App.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Autor, AutorDTO>().ReverseMap();
            CreateMap<AutorCreacionDTO, Autor>();

            CreateMap<AutorLibro, AutorLibroDTO>().ReverseMap();
            CreateMap<AutorLibroCreacionDTO, AutorLibro>();

            CreateMap<Ejemplar, EjemplarDTO>().ReverseMap();
            CreateMap<EjemplarCreacionDTO, Ejemplar>();

            CreateMap<Libro, LibroDTO>().ReverseMap();
            CreateMap<LibroCreacionDTO, Libro>();

            CreateMap<UsuarioEjemplar, UsuarioEjemplarDTO>().ReverseMap();
            CreateMap<UsuarioEjemplarCreacionDTO, UsuarioEjemplar>();
        }
    }
}
