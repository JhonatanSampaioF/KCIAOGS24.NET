using KCIAOGS24.NET.Infraestructure.Data.AppData;
using KCIAOGS24.NET.Domain.Interfaces;
using KCIAOGS24.NET.Domain.Entities;

namespace KCIAOGS24.NET.Infraestructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public UsuarioEntity? DeletarDados(int id)
        {
            try
            {
                var usuario = _context.Usuario.Find(id);

                if (usuario is not null)
                {
                    _context.Remove(usuario);
                    _context.SaveChanges();

                    return usuario;
                }
                throw new Exception("Não foi possível localizar o usuario ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public UsuarioEntity? EditarDados(UsuarioEntity entity)
        {
            try
            {
                var usuario = _context.Usuario.Find(entity.id);

                if (usuario is not null)
                {
                    usuario.nome = entity.nome;
                    usuario.email = entity.email;

                    _context.Update(usuario);
                    _context.SaveChanges();

                    return usuario;
                }

                throw new Exception("Não foi possível localizar o usuario ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public UsuarioEntity? ObterporId(int id)
        {
            var usuario = _context.Usuario.Find(id);

            if (usuario is not null)
            {
                return usuario;
            }
            return null;
        }

        public IEnumerable<UsuarioEntity>? ObterTodos()
        {
            var usuarios = _context.Usuario.ToList();

            if (usuarios.Any())
                return usuarios;

            return null;
        }

        public UsuarioEntity? SalvarDados(UsuarioEntity entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível salvar o usuario ");
            }
        }
    }
}
