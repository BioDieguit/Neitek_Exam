using Neitek.Shared.Models;

namespace Neitek.Client.IServices
{
    public interface IMetasService
    {
        Task<ApiResponse<IEnumerable<Metas>>> All(string url);
        Task<ApiResponse<Metas>> GetByDsc(string url, string metaDsc);
        Task<ApiResponse<Mensaje>> Add(string url, Metas metas);
        Task<ApiResponse<Mensaje>> Update(string url, Metas metas);
        Task<ApiResponse<Mensaje>> UpdateProgreso(string url, Metas metas);
        Task<ApiResponse<Mensaje>> Delete(string url, short id);
    }
}
