using Microsoft.EntityFrameworkCore;
using Neitek.Shared.Models;

namespace Character.Data
{
    public class DataContext : DbContext // Heredamos DbContext y para poder usarlo tenemos que usar "Microsoft.EntityFrameworkCore", es basicamente para poder utilizar el SQL Server asi como sus querys
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) // Constructor
        {

        }

        public virtual DbSet<Metas> Metas { get; set; }
        public virtual DbSet<Tareas> Tareas { get; set; }
    }
}
