using KCIAOGS24.NET.Domain.Entities;

namespace KCIAOGS24.NET.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<UsuarioEntity>? ObterTodos();
        UsuarioEntity? ObterporId(int id);
        UsuarioEntity? SalvarDados(UsuarioEntity entity);
        UsuarioEntity? EditarDados(UsuarioEntity entity);
        UsuarioEntity? DeletarDados(int id);
    }
}
