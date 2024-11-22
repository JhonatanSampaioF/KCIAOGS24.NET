using KCIAOGS24.NET.Domain.Entities;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;

namespace KCIAOGS24.NET.Application.Interfaces
{
    public interface IEnergiaEolicaApplicationService
    {
        IEnumerable<EnergiaEolicaEntity>? ObterTodasEnergiaEolicas();
        EnergiaEolicaEntity? ObterEnergiaEolicaporId(int id);
        EnergiaEolicaEntity? SalvarDadosEnergiaEolica(EnergiaEolicaDto entity);
        EnergiaEolicaEntity? EditarDadosEnergiaEolica(int id, EnergiaEolicaEditDto entity);
        EnergiaEolicaEntity? DeletarDadosEnergiaEolica(int id);
    }
}
