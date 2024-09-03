using System.ComponentModel.DataAnnotations;

namespace Neitek.Shared.Models
{
    public class Metas
    {
        [Key]
        public short Meta_Id { get; set; }
        public string Meta_Dsc { get; set; }
        public short Progreso { get; set; }
        public DateTime Fecha_Alta { get; set; }
    }
}
