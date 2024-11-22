using KCIAOGS24.NET.Infraestructure.Data.AppData;
using KCIAOGS24.NET.Domain.Interfaces;
using KCIAOGS24.NET.Domain.Entities;

namespace KCIAO.API.MVC.Infraestructure.Data.Repositories
{
    public class EnergiaSolarRepository : IEnergiaSolarRepository
    {
        private readonly ApplicationContext _context;

        public EnergiaSolarRepository(ApplicationContext context)
        {
            _context = context;
        }

        public EnergiaSolarEntity? DeletarDados(int id)
        {
            try
            {
                var energiaSolar = _context.EnergiaSolar.Find(id);

                if (energiaSolar is not null)
                {
                    _context.Remove(energiaSolar);
                    _context.SaveChanges();

                    return energiaSolar;
                }
                throw new Exception("Não foi possível localizar a energia solar ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public EnergiaSolarEntity? EditarDados(EnergiaSolarEntity entity)
        {
            try
            {
                var energiaSolar = _context.EnergiaSolar.Find(entity.id);

                if (energiaSolar is not null)
                {
                    energiaSolar.areaPlaca = entity.areaPlaca;
                    energiaSolar.irradiacaoSolar = entity.irradiacaoSolar;
                    energiaSolar.energiaEstimadaGerada = entity.energiaEstimadaGerada;
                    energiaSolar.fk_endereco = entity.fk_endereco;

                    _context.Update(energiaSolar);
                    _context.SaveChanges();

                    return energiaSolar;
                }

                throw new Exception("Não foi possível localizar a energia solar ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public EnergiaSolarEntity? ObterporId(int id)
        {
            var energiaSolar = _context.EnergiaSolar.Find(id);

            if (energiaSolar is not null)
            {
                return energiaSolar;
            }
            return null;
        }

        public IEnumerable<EnergiaSolarEntity>? ObterTodos()
        {
            var energiaSolars = _context.EnergiaSolar.ToList();

            if (energiaSolars.Any())
                return energiaSolars;

            return null;
        }

        public EnergiaSolarEntity? SalvarDados(EnergiaSolarEntity entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível salvar a energia solar ");
            }
        }
    }
}
