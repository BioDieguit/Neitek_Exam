using Neitek.Shared.Models;

namespace Neitek.IRepository
{
    public interface ITareasRepository
    {
        Task<DbResponse<IEnumerable<Tareas>>> All(short metaId);
        Task<DbResponse<Tareas>> GetByDsc(Tareas tareas);
        Task<DbResponse<Mensaje>> Add(Tareas tareas);
        Task<DbResponse<Mensaje>> Update(Tareas[] tareas);
        Task<DbResponse<Mensaje>> Complete(Tareas tareas);
        Task<DbResponse<Mensaje>> Importancia(Tareas tareas);
        Task<DbResponse<Mensaje>> DeleteAll(short metaId);
        Task<DbResponse<Mensaje>> DeleteOne(string tareaId, short metaId);
    }
}
