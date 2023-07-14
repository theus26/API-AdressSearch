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
            var  deserialize = new List<InfoCepDTO>();

            //Configurando a request
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync($"{Url}/{UF}/{city}/{logr}/json/");
                    //Valida resposta da request
                    var statusCode = response.StatusCode;

                    if(statusCode == System.Net.HttpStatusCode.OK)
                    {
                        string responseBody =  await response.Content.ReadAsStringAsync();

                        if (responseBody == "[]" || responseBody == null) throw new Exception($"Não foi possivel encontrar nada com os parametros informados");

                        try
                        {
                            deserialize = JsonConvert.DeserializeObject<List<InfoCepDTO>>(responseBody);
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

                        return deserialize; 
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Ocorreu um erro na requisição: {ex.Message}");
                }
            }
        }

        async Task<List<InfoStateDTO>> IRequests.GetUf()
        {
            var Url = _configuration.GetSection("Urls").GetSection("UrlsIBGE").Value;
            var deserialize = new List<InfoStateDTO>();

            //Iniciando a request
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync(Url);

                var status = response.IsSuccessStatusCode; // Verifica se a requisição foi bem-sucedida

                if (status == true)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    try
                    {
                        deserialize = JsonConvert.DeserializeObject<List<InfoStateDTO>>(content);
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

                return deserialize;
            }
            catch (HttpRequestException ex)
            {
                throw new ArgumentException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }

        async Task<List<InfoCityDTO>> IRequests.GetState(string UF)
        {
            var Url = _configuration.GetSection("Urls").GetSection("UrlsIBGE").Value;
            var deserialize = new List<InfoCityDTO>();

            //Iniciando a request
            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync($"{Url}/{UF}/municipios");

                var status = response.IsSuccessStatusCode; // Verifica se a requisição foi bem-sucedida

                if (status == true)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    try
                    {
                        deserialize = JsonConvert.DeserializeObject<List<InfoCityDTO>>(content);
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

                return deserialize;
            }
            catch (HttpRequestException ex)
            {
                throw new ArgumentException($"Ocorreu um erro na requisição: {ex.Message}");
            }
        }
    }
}
