using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCIAOGS24.NET.Application.Dtos.Create
{
    public class EnergiaSolarDto
    {

        [DisplayName("Area Placa")]
        [Required(ErrorMessage = $"Campo {nameof(areaPlaca)} é obrigatório")]
        public int areaPlaca { get; set; }

        [DisplayName("Irradiacao Solar")]
        [Required(ErrorMessage = $"Campo {nameof(irradiacaoSolar)} é obrigatório")]
        public Double irradiacaoSolar { get; set; }

        [DisplayName("Energia Estimada Gerada")]
        [Required(ErrorMessage = $"Campo {nameof(energiaEstimadaGerada)} é obrigatório")]
        public Double energiaEstimadaGerada { get; set; }

        [DisplayName("Endereco")]
        [Required(ErrorMessage = $"Campo {nameof(fk_endereco)} é obrigatório")]
        public int fk_endereco { get; set; }
    }
}
