using API_AdressSearch.Domain.DTO;
using API_AdressSearch.Infra.CrossCutting.Requests.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace API_AdressSearch.Infra.CrossCutting.Requests
{

    public class Requests : IRequests
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public Requests(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

         async Task<List<InfoCepDTO>?> IRequests.GetCep(DataDTO data)
        {
            var url = _configuration.GetSection("Urls").GetSection("UrlCep").Value;
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync($"{url}/{data.Uf}/{data.City}/{data.Logre}/json/");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Não foi possivel realizar a request");

                var responseBody =  await response.Content.ReadAsStringAsync();

                if (responseBody is "[]" or null) throw new Exception($"Não foi possivel encontrar nada com os parametros informados");

                try
                {
                    return JsonConvert.DeserializeObject<List<InfoCepDTO>>(responseBody);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao deserializar objeto: {ex.Message} ");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        async Task<List<InfoStateDTO>?> IRequests.GetUf()
        {
            var url = _configuration.GetSection("Urls").GetSection("UrlsIBGE").Value;
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Não foi possivel realizar a request");

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<InfoStateDTO>>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        async Task<List<InfoCityDTO>?> IRequests.GetState(string uf)
        {
            var url = _configuration.GetSection("Urls").GetSection("UrlsIBGE").Value;
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync($"{url}/{uf}/municipios");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Não foi possivel realizar a request");

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<InfoCityDTO>>(content);

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }
    }
}
