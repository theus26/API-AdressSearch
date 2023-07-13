using API_AdressSearch.Domain.DTO;


namespace API_AdressSearch.Infra.CrossCutting.Requests.Interfaces
{
    public interface IRequests
    {
        Task<List<InfoCepDTO>> GetCep(string UF, string city, string logr);
        Task<List<InfoStateDTO>> GetUf();
        Task<List<InfoCityDTO>> GetState(string UF);
    }
}
