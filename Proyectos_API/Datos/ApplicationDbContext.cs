using Microsoft.EntityFrameworkCore;
using Proyectos_API.Models;

namespace Proyectos_API.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                    
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id=1,
                    Nombre="Villa nueva",
                    Detalle="Detalle de la Villa",
                    Ocupantes=5,
                    MetrosCuadrados=5,
                    Amenidad="#",
                    ImagenURL="#",
                    Tarifa=200,
                    FechaActualizacion=DateTime.Now,
                    FechaCreacion=DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "Villa real",
                    Detalle = "Detalle de la Villa",
                    Ocupantes = 10,
                    MetrosCuadrados = 16,
                    Amenidad = "#",
                    ImagenURL = "#",
                    Tarifa = 400,
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                }
            );
        }

    }
}
