using System.ComponentModel.DataAnnotations;

namespace Neitek.Shared.Models
{
    public class Tareas
    {
        [Key]
        public string Tarea_Id { get; set; }
        public DateTime Fecha_Alta { get; set; }
        public bool Estado_Id { get; set; }
        public short Meta_Id { get; set; }
        public bool Importancia { get; set; }
    }
}
