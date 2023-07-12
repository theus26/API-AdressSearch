using API_AdressSearch.Domain.DTO;
using API_AdressSearch.Infra.CrossCutting.Requests.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Serialization;

namespace API_AdressSearch.Infra.CrossCutting.Requests
{
   
    public class Requests : IRequests
    {
        private readonly IConfiguration _configuration;
        public Requests(IConfiguration configuration)
        {
            _configuration = configuration;
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

                    return deserialize; 
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Erro ao deserializar objeto: {ex.Message} ");
                }
            }
        }

        Task<InfoStateDTO> IRequests.GetState()
        {
            throw new NotImplementedException();
        }

        Task<InfoUfDTO> IRequests.GetUf()
        {
            throw new NotImplementedException();
        }
    }
}
