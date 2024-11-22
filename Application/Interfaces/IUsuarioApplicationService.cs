using KCIAOGS24.NET.Domain.Entities;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;

namespace KCIAOGS24.NET.Application.Interfaces
{
    public interface IUsuarioApplicationService
    {
        IEnumerable<UsuarioEntity>? ObterTodasUsuarios();
        UsuarioEntity? ObterUsuarioporId(int id);
        UsuarioEntity? SalvarDadosUsuario(UsuarioDto entity);
        UsuarioEntity? EditarDadosUsuario(int id, UsuarioEditDto entity);
        UsuarioEntity? DeletarDadosUsuario(int id);
    }
}
