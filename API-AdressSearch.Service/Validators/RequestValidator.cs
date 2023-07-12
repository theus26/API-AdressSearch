using API_AdressSearch.Domain.DTO;
using API_AdressSearch.Service.Validators.Interface;
using FluentValidation;
using System.Text.RegularExpressions;

namespace API_AdressSearch.Service.Validators
{
    public class RequestValidator : IRequestValidator
    {
        public void Validate(DataDTO data)
        {
            //Validação
            if (string.IsNullOrEmpty(data.Uf)) throw new ArgumentException("UF can´t be empty or null");
            if (string.IsNullOrEmpty(data.City)) throw new ArgumentException("city can´t be empty or null");
            if (string.IsNullOrEmpty(data.logre)) throw new ArgumentException("logre can´t be empty or null");


            //Regex
            var uf = new Regex(@"^[a-zA-Z]+");
            var ufMatches = uf.Matches(data.Uf);
            if (ufMatches.Count == 0) throw new ArgumentException("Please enter a UF valid");

            var city = new Regex(@"^[a-zA-Z]+");
            var cityMatches = city.Matches(data.Uf);
            if (cityMatches.Count == 0) throw new ArgumentException("Please enter a UF CITY");

            var logre = new Regex(@"^[a-zA-Z]+");
            var logreMatches = logre.Matches(data.logre);
            if (logreMatches.Count == 0) throw new ArgumentException("Please enter a UF valid");

        }
    }
}
