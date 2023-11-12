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

         async Task<List<InfoCepDTO>> IRequests.GetCep(string UF, string city, string logr)
        {
            var Url = _configuration.GetSection("Urls").GetSection("UrlCep").Value;
            var  Deserialize = new List<InfoCepDTO>();

            //Configurando a request
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync($"{Url}/{UF}/{city}/{logr}/json/");
                    

                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string responseBody =  await response.Content.ReadAsStringAsync();

                        if (responseBody == "[]" || responseBody is null) throw new Exception($"Não foi possivel encontrar nada com os parametros informados");

                        try
                        {
                            Deserialize = JsonConvert.DeserializeObject<List<InfoCepDTO>>(responseBody);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Erro ao deserializar objeto: {ex.Message} ");
                        }
                    }
                    else
                    {
                        throw new HttpRequestException($"Não foi possivel realizar a request");

                    }

                        return Deserialize; 
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
                }
            }
        }

        async Task<List<InfoStateDTO>> IRequests.GetUf()
        {
            var Url = _configuration.GetSection("Urls").GetSection("UrlsIBGE").Value;
            var Deserialize = new List<InfoStateDTO>();

            //Iniciando a request
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(Url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    try
                    {
                        Deserialize = JsonConvert.DeserializeObject<List<InfoStateDTO>>(content);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                        throw new HttpRequestException($"Não foi possivel realizar a request");
                }

                return Deserialize;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        async Task<List<InfoCityDTO>> IRequests.GetState(string UF)
        {
            var Url = _configuration.GetSection("Urls").GetSection("UrlsIBGE").Value;
            var Deserialize = new List<InfoCityDTO>();

            //Iniciando a request
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync($"{Url}/{UF}/municipios");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    try
                    {
                        Deserialize = JsonConvert.DeserializeObject<List<InfoCityDTO>>(content);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    throw new HttpRequestException($"Não foi possivel realizar a request");
                }

                return Deserialize;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }
    }
}
