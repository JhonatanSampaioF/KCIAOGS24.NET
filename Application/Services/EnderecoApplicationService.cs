using KCIAOGS24.NET.Domain.Entities;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;
using KCIAOGS24.NET.Application.Interfaces;
using KCIAOGS24.NET.Domain.Interfaces;

namespace KCIAOGS24.NET.Application.Services
{
    public class EnderecoApplicationService : IEnderecoApplicationService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoApplicationService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public EnderecoEntity? DeletarDadosEndereco(int id)
        {
            return _enderecoRepository.DeletarDados(id);
        }

        public EnderecoEntity? EditarDadosEndereco(int id, EnderecoEditDto entity)
        {
            var endereco = new EnderecoEntity
            {
                id = id,
                tipoResidencial = entity.tipoResidencial,
                nome = entity.nome,
                cep = entity.cep,
                tarifa = entity.tarifa,
                gastoMensal = entity.gastoMensal,
                economia = entity.economia,
                fk_usuario = entity.fk_usuario
            };

            return _enderecoRepository.EditarDados(endereco);
        }

        public EnderecoEntity? ObterEnderecoporId(int id)
        {
            return _enderecoRepository.ObterporId(id);
        }

        public IEnumerable<EnderecoEntity>? ObterTodasEnderecos()
        {
            return _enderecoRepository.ObterTodos();
        }

        public EnderecoEntity? SalvarDadosEndereco(EnderecoDto entity)
        {
            var endereco = new EnderecoEntity
            {
                tipoResidencial = entity.tipoResidencial,
                nome = entity.nome,
                cep = entity.cep,
                tarifa = entity.tarifa,
                gastoMensal = entity.gastoMensal,
                economia = entity.economia,
                fk_usuario = entity.fk_usuario
            };

            return _enderecoRepository.SalvarDados(endereco);
        }
    }
}
