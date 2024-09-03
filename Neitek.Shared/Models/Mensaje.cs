namespace Neitek.Shared.Models
{
    public partial class Mensaje
    {
        public byte TipoMensaje { get; set; }
        public string Texto { get; set; }

        public Mensaje()
        {
            Texto = string.Empty;
        }
    }
}
