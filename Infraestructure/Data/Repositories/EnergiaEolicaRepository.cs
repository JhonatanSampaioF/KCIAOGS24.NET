using KCIAOGS24.NET.Infraestructure.Data.AppData;
using KCIAOGS24.NET.Domain.Interfaces;
using KCIAOGS24.NET.Domain.Entities;

namespace KCIAO.API.MVC.Infraestructure.Data.Repositories
{
    public class EnergiaEolicaRepository : IEnergiaEolicaRepository
    {
        private readonly ApplicationContext _context;

        public EnergiaEolicaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public EnergiaEolicaEntity? DeletarDados(int id)
        {
            try
            {
                var energiaEolica = _context.EnergiaEolica.Find(id);

                if (energiaEolica is not null)
                {
                    _context.Remove(energiaEolica);
                    _context.SaveChanges();

                    return energiaEolica;
                }
                throw new Exception("Não foi possível localizar a energia eolica ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public EnergiaEolicaEntity? EditarDados(EnergiaEolicaEntity entity)
        {
            try
            {
                var energiaEolica = _context.EnergiaEolica.Find(entity.id);

                if (energiaEolica is not null)
                {
                    energiaEolica.potencialNominal = entity.potencialNominal;
                    energiaEolica.alturaTorre = entity.alturaTorre;
                    energiaEolica.diametroRotor = entity.diametroRotor;
                    energiaEolica.energiaEstimadaGerada = entity.energiaEstimadaGerada;
                    energiaEolica.fk_endereco = entity.fk_endereco;

                    _context.Update(energiaEolica);
                    _context.SaveChanges();

                    return energiaEolica;
                }

                throw new Exception("Não foi possível localizar a energia eolica ");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public EnergiaEolicaEntity? ObterporId(int id)
        {
            var energiaEolica = _context.EnergiaEolica.Find(id);

            if (energiaEolica is not null)
            {
                return energiaEolica;
            }
            return null;
        }

        public IEnumerable<EnergiaEolicaEntity>? ObterTodos()
        {
            var energiaEolicas = _context.EnergiaEolica.ToList();

            if (energiaEolicas.Any())
                return energiaEolicas;

            return null;
        }

        public EnergiaEolicaEntity? SalvarDados(EnergiaEolicaEntity entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();

                return entity;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível salvar a energia eolica ");
            }
        }
    }
}
