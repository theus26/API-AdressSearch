using API_AdressSearch.Domain.DTO;
using API_AdressSearch.Service.Validators.Interface;
using FluentValidation;
using System.Text.RegularExpressions;

namespace API_AdressSearch.Service.Validators
{
    public class RequestValidator : IRequestValidator
    {
        public void Validate(string Uf, string City, string logre)
        {
            //Validação
            if (string.IsNullOrEmpty(Uf)) throw new ArgumentException("UF can´t be empty or null");
            if (string.IsNullOrEmpty(City)) throw new ArgumentException("city can´t be empty or null");
            if (string.IsNullOrEmpty(logre)) throw new ArgumentException("logre can´t be empty or null");


            //Regex
            var uf = new Regex(@"^[a-zA-Z]+");
            var ufMatches = uf.Matches(Uf);
            if (ufMatches.Count == 0) throw new ArgumentException("Please enter a UF valid");

            var city = new Regex(@"^[a-zA-Z]+");
            var cityMatches = city.Matches(City);
            if (cityMatches.Count == 0) throw new ArgumentException("Please enter a city valid");

            var logres = new Regex(@"^[a-zA-Z]+");
            var logreMatches = logres.Matches(logre);
            if (logreMatches.Count == 0) throw new ArgumentException("Please enter a logre valid");

        }
    }
}
