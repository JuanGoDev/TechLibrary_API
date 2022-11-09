
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechLibrary_App.Entidades;
using TechLibrary_App.Models;

namespace TechLibrary_App.Data
{
    public class ApplicationDbContext: IdentityDbContext<Usuario>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }     
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutorLibro>()
                .HasOne(bc => bc.Autor)
                .WithMany(b => b.AutoresLibros)
                .HasForeignKey(bc => bc.IdAutor);
            modelBuilder.Entity<AutorLibro>()
                .HasOne(bc => bc.Libro)
                .WithMany(b => b.AutoresLibros)
                .HasForeignKey(bc => bc.IdLibro);

            modelBuilder.Entity<Ejemplar>()
                .HasOne(bc => bc.Libro)
                .WithMany(b => b.Ejemplares)
                .HasForeignKey(bc => bc.IdLibro);

            modelBuilder.Entity<UsuarioEjemplar>()
                .HasOne(bc => bc.Usuarios)
                .WithMany(b => b.UsuarioEjemplares)
                .HasForeignKey(bc => bc.IdUsuario);
            modelBuilder.Entity<UsuarioEjemplar>()
                .HasOne(bc => bc.Ejemplares)
                .WithMany(b => b.UsuarioEjemplares)
                .HasForeignKey(bc => bc.IdEjemplar);
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<AutorLibro> AutoresLibros { get; set; }
        public DbSet<Ejemplar> Ejemplares { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<UsuarioEjemplar> UsuariosEjemplares { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
