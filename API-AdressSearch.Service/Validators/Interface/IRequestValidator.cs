using API_AdressSearch.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_AdressSearch.Service.Validators.Interface
{
    public interface IRequestValidator
    {
        void Validate(string UF, string City, string logre);
    }
}
