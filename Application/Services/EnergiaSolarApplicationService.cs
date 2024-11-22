using KCIAOGS24.NET.Domain.Entities;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;
using KCIAOGS24.NET.Application.Interfaces;
using KCIAOGS24.NET.Domain.Interfaces;

namespace KCIAOGS24.NET.Application.Services
{
    public class EnergiaSolarApplicationService : IEnergiaSolarApplicationService
    {
        private readonly IEnergiaSolarRepository _energiaSolarRepository;

        public EnergiaSolarApplicationService(IEnergiaSolarRepository energiaSolarRepository)
        {
            _energiaSolarRepository = energiaSolarRepository;
        }

        public EnergiaSolarEntity? DeletarDadosEnergiaSolar(int id)
        {
            return _energiaSolarRepository.DeletarDados(id);
        }

        public EnergiaSolarEntity? EditarDadosEnergiaSolar(int id, EnergiaSolarEditDto entity)
        {
            var energiaSolar = new EnergiaSolarEntity
            {
                id = id,
                areaPlaca = entity.areaPlaca,
                irradiacaoSolar = entity.irradiacaoSolar,
                energiaEstimadaGerada = entity.energiaEstimadaGerada,
                fk_endereco = entity.fk_endereco
            };

            return _energiaSolarRepository.EditarDados(energiaSolar);
        }

        public EnergiaSolarEntity? ObterEnergiaSolarporId(int id)
        {
            return _energiaSolarRepository.ObterporId(id);
        }

        public IEnumerable<EnergiaSolarEntity>? ObterTodasEnergiaSolars()
        {
            return _energiaSolarRepository.ObterTodos();
        }

        public EnergiaSolarEntity? SalvarDadosEnergiaSolar(EnergiaSolarDto entity)
        {
            var energiaSolar = new EnergiaSolarEntity
            {
                areaPlaca = entity.areaPlaca,
                irradiacaoSolar = entity.irradiacaoSolar,
                energiaEstimadaGerada = entity.energiaEstimadaGerada,
                fk_endereco = entity.fk_endereco
            };

            return _energiaSolarRepository.SalvarDados(energiaSolar);
        }
    }
}
