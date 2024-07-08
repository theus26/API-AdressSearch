using API_AdressSearch.Domain.DTO;
using API_AdressSearch.Service.Validators.Interface;
using System.Text.RegularExpressions;

namespace API_AdressSearch.Service.Validators
{
    public class Validator : IValidator
    {
        public void Validate(DataDTO dataDto)
        {
            //Validação
            if (string.IsNullOrEmpty(dataDto.Uf)) throw new ArgumentException("UF can´t be empty or null");
            if (string.IsNullOrEmpty(dataDto.City)) throw new ArgumentException("city can´t be empty or null");
            if (string.IsNullOrEmpty(dataDto.Logre)) throw new ArgumentException("logre can´t be empty or null");


            //Regex
            var uf = new Regex(@"^[a-zA-Z]+");
            var ufMatches = uf.Matches(dataDto.Uf);
            if (ufMatches.Count == 0) throw new ArgumentException("Please enter a UF valid");

            var city = new Regex(@"^[a-zA-Z]+");
            var cityMatches = city.Matches(dataDto.City);
            if (cityMatches.Count == 0) throw new ArgumentException("Please enter a city valid");

            var logres = new Regex(@"^[a-zA-Z]+");
            var logreMatches = logres.Matches(dataDto.Logre);
            if (logreMatches.Count == 0) throw new ArgumentException("Please enter a logre valid");

        }
    }
}
