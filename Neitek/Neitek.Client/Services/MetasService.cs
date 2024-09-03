using Neitek.Client.IServices;
using Neitek.Shared.Models;
using System;
using System.Net.Http.Json;
using System.Text.Json;

namespace Neitek.Client.Services
{
    public class MetasService : IMetasService
    {
        private readonly HttpClient _httpClient;

        public MetasService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<ApiResponse<IEnumerable<Metas>>> All(string url)
        {
            string cadena = url + "api/Metas/All";
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<Metas>>>(cadena);
            return result;
        }
        public async Task<ApiResponse<Metas>> GetByDsc(string url, string metaDsc)
        {
            string cadena = String.Format(url + "api/Metas/GetByDsc/{0}", metaDsc);
            var result = await _httpClient.GetFromJsonAsync<ApiResponse<Metas>>(cadena);
            return result;
        }
        public async Task<ApiResponse<Mensaje>> Add(string url, Metas metas)
        {
            string cadena = url + "api/Metas/Add";
            var response = await _httpClient.PostAsJsonAsync(cadena, metas);
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
        public async Task<ApiResponse<Mensaje>> Update(string url, Metas metas)
        {
            string cadena = url + "api/Metas/Update";
            var response = await _httpClient.PutAsJsonAsync(cadena, metas);
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
        public async Task<ApiResponse<Mensaje>> UpdateProgreso(string url, Metas metas)
        {
            string cadena = url + "api/Metas/Progreso";
            var response = await _httpClient.PutAsJsonAsync(cadena, metas);
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
        public async Task<ApiResponse<Mensaje>> Delete(string url, short id)
        {
            string cadena = String.Format(url + "api/Metas/Delete?id={0}", id);
            var deleteResponse = await _httpClient.DeleteAsync(cadena);
            string deleteResponseString = await deleteResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<Mensaje>>(deleteResponseString);
            return result;
        }    
    }
}
