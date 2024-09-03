using Neitek.Shared.Models;

namespace Neitek.Client.IServices
{
    public interface ITareasService
    {
        Task<ApiResponse<IEnumerable<Tareas>>> All(short metaId, string metaDsc);
        Task<ApiResponse<Mensaje>> Add(string url, Tareas Tareas);
        Task<ApiResponse<Mensaje>> Update(string url, Tareas[] tareas);
        Task<ApiResponse<Mensaje>> Complete(string url, Tareas tareas);
        Task<ApiResponse<Mensaje>> Importancia(string url, Tareas tareas);
        Task<ApiResponse<Mensaje>> DeleteAll(string url, short metaId);
        Task<ApiResponse<Mensaje>> DeleteOne(string url, string tareaId, short metaId);
    }
}

