using API_AdressSearch.Domain.DTO;

namespace API_AdressSearch.Service.Validators.Interface
{
    public interface IValidator
    {
        void Validate(DataDTO dataDto);
    }
}
