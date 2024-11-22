using KCIAOGS24.NET.Domain.Entities;

namespace KCIAOGS24.NET.Domain.Interfaces
{
    public interface IEnergiaEolicaRepository
    {
        IEnumerable<EnergiaEolicaEntity>? ObterTodos();
        EnergiaEolicaEntity? ObterporId(int id);
        EnergiaEolicaEntity? SalvarDados(EnergiaEolicaEntity entity);
        EnergiaEolicaEntity? EditarDados(EnergiaEolicaEntity entity);
        EnergiaEolicaEntity? DeletarDados(int id);
    }
}
