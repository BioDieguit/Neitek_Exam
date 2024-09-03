using Neitek.Client.IServices;
using Neitek.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Neitek.Client.Services
{
    public class TareasService : ITareasService
    {
        private readonly HttpClient _httpClient;

        public TareasService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<ApiResponse<IEnumerable<Tareas>>> All(short metaId, string url)
        {
            string cadena = String.Format(url + "api/Tareas/All?metaId={0}", metaId);
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<Tareas>>>(cadena);
            return result;
        }
        public async Task<ApiResponse<Mensaje>> Add(string url, Tareas metas)
        {
            string cadena = url + "api/Tareas/Add";
            var response = await _httpClient.PostAsJsonAsync(cadena, metas);
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
        public async Task<ApiResponse<Mensaje>> Update(string url, Tareas[] tareas)
        {
            string cadena = url + "api/Tareas/UpdateTarea";
            var response = await _httpClient.PutAsJsonAsync(cadena,tareas);
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
        public async Task<ApiResponse<Mensaje>> Complete(string url, Tareas tareas)
        {
            string cadena = url + "api/Tareas/Complete";
            var response = await _httpClient.PutAsJsonAsync(cadena, tareas);
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
        public async Task<ApiResponse<Mensaje>> Importancia(string url, Tareas tareas)
        {
            string cadena = url + "api/Tareas/Importancia";
            var response = await _httpClient.PutAsJsonAsync(cadena, tareas);
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
        public async Task<ApiResponse<Mensaje>> DeleteAll(string url, short metaId)
        {
            string cadena = String.Format(url + "api/Tareas/DeleteAll?metaId={0}", metaId);
            var deleteResponse = await _httpClient.DeleteAsync(cadena);
            string deleteResponseString = await deleteResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(deleteResponseString);
            return result;
        }
        public async Task<ApiResponse<Mensaje>> DeleteOne(string url, string tareaId, short metaId)
        {
            string cadena = String.Format(url + "api/Tareas/DeleteOne?tareaId={0}&metaId={1}", tareaId, metaId);
            var deleteResponse = await _httpClient.DeleteAsync(cadena);
            string deleteResponseString = await deleteResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(deleteResponseString);
            return result;
        }
    }
}
