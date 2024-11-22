using KCIAOGS24.NET.Domain.Entities;

namespace KCIAOGS24.NET.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        IEnumerable<EnderecoEntity>? ObterTodos();
        EnderecoEntity? ObterporId(int id);
        EnderecoEntity? SalvarDados(EnderecoEntity entity);
        EnderecoEntity? EditarDados(EnderecoEntity entity);
        EnderecoEntity? DeletarDados(int id);
    }
}
