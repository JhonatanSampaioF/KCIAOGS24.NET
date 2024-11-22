using KCIAOGS24.NET.Domain.Entities;

namespace KCIAOGS24.NET.Domain.Interfaces
{
    public interface IEnergiaSolarRepository
    {
        IEnumerable<EnergiaSolarEntity>? ObterTodos();
        EnergiaSolarEntity? ObterporId(int id);
        EnergiaSolarEntity? SalvarDados(EnergiaSolarEntity entity);
        EnergiaSolarEntity? EditarDados(EnergiaSolarEntity entity);
        EnergiaSolarEntity? DeletarDados(int id);
    }
}
