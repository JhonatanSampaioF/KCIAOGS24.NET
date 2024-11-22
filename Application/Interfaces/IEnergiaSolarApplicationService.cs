using KCIAOGS24.NET.Domain.Entities;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;

namespace KCIAOGS24.NET.Application.Interfaces
{
    public interface IEnergiaSolarApplicationService
    {
        IEnumerable<EnergiaSolarEntity>? ObterTodasEnergiaSolars();
        EnergiaSolarEntity? ObterEnergiaSolarporId(int id);
        EnergiaSolarEntity? SalvarDadosEnergiaSolar(EnergiaSolarDto entity);
        EnergiaSolarEntity? EditarDadosEnergiaSolar(int id, EnergiaSolarEditDto entity);
        EnergiaSolarEntity? DeletarDadosEnergiaSolar(int id);
    }
}
