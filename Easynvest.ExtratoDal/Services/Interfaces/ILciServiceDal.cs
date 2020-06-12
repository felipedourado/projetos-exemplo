using Easynvest.ExtratoDal.Dtos;

namespace Easynvest.ExtratoDal.Services.Interfaces
{
    public interface ILciServiceDal
    {
        LciDtoOutput Consultar();
    }
}
