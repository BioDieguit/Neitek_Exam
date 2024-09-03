using System.ComponentModel.DataAnnotations;

namespace Neitek.Shared.Models
{
    public enum TipoMensaje
    {
        [Display(Name = "")]
        Ninguno = 0,
        [Display(Name = "Error")]
        Error = 1,
        [Display(Name = "¿?")]
        Pregunta = 2,
        [Display(Name = "Alerta")]
        Alerta = 3,
        [Display(Name = "Información")]
        Informacion = 4,
        [Display(Name = "Exitoso")]
        Exitoso = 5
    }
}