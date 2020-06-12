using Easynvest.ExtratoDal.Dtos;

namespace Easynvest.ExtratoDal.Services.Interfaces
{
    public interface IFundoServiceDal
    {
        FundoDtoOutput Consultar();
    }
}
