using Easynvest.ExtratoDal.Dtos;

namespace Easynvest.ExtratoDal.Services.Interfaces
{
    public interface ITesouroServiceDal
    {
        TesouroDtoOutput Consultar();
    }
}
