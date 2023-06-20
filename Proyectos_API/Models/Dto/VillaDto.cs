using System.ComponentModel.DataAnnotations;

namespace Proyectos_API.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public string Detalle { get; set; }
        public string Amenidad { get; set; }
        public string ImagenURL { get; set; }
    }
}
