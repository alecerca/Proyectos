using System.ComponentModel.DataAnnotations;

namespace Proyectos_API.Models.Dto
{
    public class NumeroVillaDtoUpdate
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetallesEspeciales { get; set; }
    }
}
