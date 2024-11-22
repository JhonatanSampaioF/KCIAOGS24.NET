using KCIAOGS24.NET.Domain.Entities;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;

namespace KCIAOGS24.NET.Application.Interfaces
{
    public interface IEnderecoApplicationService
    {
        IEnumerable<EnderecoEntity>? ObterTodasEnderecos();
        EnderecoEntity? ObterEnderecoporId(int id);
        EnderecoEntity? SalvarDadosEndereco(EnderecoDto entity);
        EnderecoEntity? EditarDadosEndereco(int id, EnderecoEditDto entity);
        EnderecoEntity? DeletarDadosEndereco(int id);
    }
}
