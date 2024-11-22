using KCIAOGS24.NET.Infraestructure.Data.AppData;
using KCIAOGS24.NET.Domain.Interfaces;
using KCIAOGS24.NET.Domain.Entities;

namespace KCIAOGS24.NET.Infraestructure.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApplicationContext _context;

        public EnderecoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public EnderecoEntity? DeletarDados(int id)
        {
            try
            {
                var endereco = _context.Endereco.Find(id);

                if (endereco is not null)
                {
                    _context.Remove(endereco);
                    _context.SaveChanges();

                    return endereco;
                }
                throw new Exception("Não foi possível localizar o endereco ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public EnderecoEntity? EditarDados(EnderecoEntity entity)
        {
            try
            {
                var endereco = _context.Endereco.Find(entity.id);

                if (endereco is not null)
                {
                    endereco.tipoResidencial = entity.tipoResidencial;
                    endereco.nome = entity.nome;
                    endereco.cep = entity.cep;
                    endereco.tarifa = entity.tarifa;
                    endereco.gastoMensal = entity.gastoMensal;
                    endereco.economia = entity.economia;
                    endereco.fk_usuario = entity.fk_usuario;

                    _context.Update(endereco);
                    _context.SaveChanges();

                    return endereco;
                }

                throw new Exception("Não foi possível localizar o endereco ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public EnderecoEntity? ObterporId(int id)
        {
            var endereco = _context.Endereco.Find(id);

            if (endereco is not null)
            {
                return endereco;
            }
            return null;
        }

        public IEnumerable<EnderecoEntity>? ObterTodos()
        {
            var enderecos = _context.Endereco.ToList();

            if (enderecos.Any())
                return enderecos;

            return null;
        }

        public EnderecoEntity? SalvarDados(EnderecoEntity entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível salvar o endereco ");
            }
        }
    }
}
