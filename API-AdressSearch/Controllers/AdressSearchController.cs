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
        private readonly IValidator _validator;
        public AdressSearchController(IRequests requests, IValidator validator )
        {
            _requests = requests;
            _validator = validator;
        }
        [HttpGet]
        public ActionResult HelthCheck()
        {
            return Ok("I'm alive and working");
        }

        [HttpGet]
        public async Task<IActionResult> GetAdressForInfo([FromQuery] DataDTO data)
        {
            try
            {
                _validator.Validate(data);
                var request = await _requests.GetCep(data);

                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseErrorDTO()
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
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseErrorDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = ex.Message,

                });
            }
        }


        [HttpGet("uf")]
        public async Task<IActionResult> GetInfoCity([FromQuery] string uf)
        {
            try
            {
                var request = await _requests.GetState(uf);
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseErrorDTO()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Error = ex.Message,

                });
            }
        }
    } 
}
