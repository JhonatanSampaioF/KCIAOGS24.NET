using KCIAOGS24.NET.Domain.Entities;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;
using KCIAOGS24.NET.Application.Interfaces;

namespace KCIAOGS24.NET.Application.Services
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioApplicationService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public UsuarioEntity? DeletarDadosUsuario(int id)
        {
            return _usuarioRepository.DeletarDados(id);
        }

        public UsuarioEntity? EditarDadosUsuario(int id, UsuarioEditDto entity)
        {
            var usuario = new UsuarioEntity
            {
                id = id,
                nome = entity.nome,
                email = entity.email
            };

            return _usuarioRepository.EditarDados(usuario);
        }

        public UsuarioEntity? ObterUsuarioporId(int id)
        {
            return _usuarioRepository.ObterporId(id);
        }

        public IEnumerable<UsuarioEntity>? ObterTodasUsuarios()
        {
            return _usuarioRepository.ObterTodos();
        }

        public UsuarioEntity? SalvarDadosUsuario(UsuarioDto entity)
        {
            var usuario = new UsuarioEntity
            {
                nome = entity.nome,
                email = entity.email
            };

            return _usuarioRepository.SalvarDados(usuario);
        }
    }
}
