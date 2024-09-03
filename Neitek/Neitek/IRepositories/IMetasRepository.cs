using Neitek.Shared.Models;

namespace Neitek.IRepository
{
    public interface IMetasRepository
    {
        Task<DbResponse<IEnumerable<Metas>>> All();
        Task<DbResponse<Metas>> GetByDsc(string metaDsc);
        Task<DbResponse<Mensaje>> Add(Metas metas);
        Task<DbResponse<Mensaje>> Update(Metas metas);
        Task<DbResponse<Mensaje>> UpdateProgreso(Metas metas);
        Task<DbResponse<Mensaje>> Delete(short id);
    }
}
