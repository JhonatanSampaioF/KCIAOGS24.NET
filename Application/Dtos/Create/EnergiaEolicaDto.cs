using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCIAOGS24.NET.Application.Dtos.Create
{
    public class EnergiaEolicaDto
    {

        [DisplayName("Potencial Nominal")]
        [Required(ErrorMessage = $"Campo {nameof(potencialNominal)} é obrigatório")]
        public Double potencialNominal { get; set; }

        [DisplayName("Altura Torre")]
        [Required(ErrorMessage = $"Campo {nameof(alturaTorre)} é obrigatório")]
        public Double alturaTorre { get; set; }

        [DisplayName("Diametro Rotor")]
        [Required(ErrorMessage = $"Campo {nameof(diametroRotor)} é obrigatório")]
        public Double diametroRotor { get; set; }

        [DisplayName("Energia Estimada Gerada")]
        [Required(ErrorMessage = $"Campo {nameof(energiaEstimadaGerada)} é obrigatório")]
        public Double energiaEstimadaGerada { get; set; }

        [DisplayName("Endereco")]
        [Required(ErrorMessage = $"Campo {nameof(fk_endereco)} é obrigatório")]
        public int fk_endereco { get; set; }
    }
}
