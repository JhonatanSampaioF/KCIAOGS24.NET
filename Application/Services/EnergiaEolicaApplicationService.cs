using KCIAOGS24.NET.Domain.Entities;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;
using KCIAOGS24.NET.Application.Interfaces;
using KCIAOGS24.NET.Domain.Interfaces;

namespace KCIAOGS24.NET.Application.Services
{
    public class EnergiaEolicaApplicationService : IEnergiaEolicaApplicationService
    {
        private readonly IEnergiaEolicaRepository _energiaEolicaRepository;

        public EnergiaEolicaApplicationService(IEnergiaEolicaRepository energiaEolicaRepository)
        {
            _energiaEolicaRepository = energiaEolicaRepository;
        }

        public EnergiaEolicaEntity? DeletarDadosEnergiaEolica(int id)
        {
            return _energiaEolicaRepository.DeletarDados(id);
        }

        public EnergiaEolicaEntity? EditarDadosEnergiaEolica(int id, EnergiaEolicaEditDto entity)
        {
            var energiaEolica = new EnergiaEolicaEntity
            {
                id = id,
                potencialNominal = entity.potencialNominal,
                alturaTorre = entity.alturaTorre,
                diametroRotor = entity.diametroRotor,
                energiaEstimadaGerada = entity.energiaEstimadaGerada,
                fk_endereco = entity.fk_endereco
            };

            return _energiaEolicaRepository.EditarDados(energiaEolica);
        }

        public EnergiaEolicaEntity? ObterEnergiaEolicaporId(int id)
        {
            return _energiaEolicaRepository.ObterporId(id);
        }

        public IEnumerable<EnergiaEolicaEntity>? ObterTodasEnergiaEolicas()
        {
            return _energiaEolicaRepository.ObterTodos();
        }

        public EnergiaEolicaEntity? SalvarDadosEnergiaEolica(EnergiaEolicaDto entity)
        {
            var energiaEolica = new EnergiaEolicaEntity
            {
                potencialNominal = entity.potencialNominal,
                alturaTorre = entity.alturaTorre,
                diametroRotor = entity.diametroRotor,
                energiaEstimadaGerada = entity.energiaEstimadaGerada,
                fk_endereco = entity.fk_endereco
            };

            return _energiaEolicaRepository.SalvarDados(energiaEolica);
        }
    }
}
