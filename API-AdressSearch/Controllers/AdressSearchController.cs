using API_AdressSearch.Domain.DTO;
using API_AdressSearch.Infra.CrossCutting.Requests.Interfaces;
using API_AdressSearch.Service.Validators.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API_AdressSearch.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdressSearchController : Controller
    {
        private readonly IRequests _requests;
        private readonly IRequestValidator _validator;
        public AdressSearchController(IRequests requests, IRequestValidator requestValidator )
        {
            _requests = requests;
            _validator = requestValidator;
        }
        [HttpGet]
        public ActionResult HelthCheck()
        {
            return Ok("I'm alive and working");
        }

        [HttpGet]
        public async Task<IActionResult> GetAdressForInfo(DataDTO data)
        {
            try
            {
                //Valida Dados 
                _validator.Validate(data.Uf, data.City, data.logre);
                //Inicia a request
                var request = await _requests.GetCep(data.Uf, data.City, data.logre);

                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new RespostaErroDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = ex.Message,
                   
                });
            }  
        }

        [HttpGet]
        public async Task<IActionResult> GetInfoStates()
        {
            try
            {
                var request = await _requests.GetUf();
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new RespostaErroDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = ex.Message,

                });
            }
        }


        [HttpGet("{UF}")]
        public async Task<IActionResult> GetInfoCity(string UF)
        {
            try
            {
                var request = await _requests.GetState(UF);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new RespostaErroDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = ex.Message,

                });
            }
        }
    } 
}
