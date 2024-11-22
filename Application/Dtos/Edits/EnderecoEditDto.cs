using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KCIAOGS24.NET.Application.Dtos.Edits
{
    public class EnderecoEditDto
    {
        [DisplayName("Id")]
        [Required(ErrorMessage = $"Campo {nameof(id)} é obrigatório")]
        public int id { get; set; }

        [DisplayName("Tipo Residencial")]
        [Required(ErrorMessage = $"Campo {nameof(tipoResidencial)} é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Campo deve ter no mínimo 3 caracteres")]
        public string tipoResidencial { get; set; } = string.Empty;

        [DisplayName("Nome")]
        [Required(ErrorMessage = $"Campo {nameof(nome)} é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Campo deve ter no mínimo 3 caracteres")]
        public string nome { get; set; } = string.Empty;

        [DisplayName("CEP")]
        [Required(ErrorMessage = $"Campo {nameof(cep)} é obrigatório")]
        public string cep { get; set; } = string.Empty;

        [DisplayName("Tarifa")]
        [Required(ErrorMessage = $"Campo {nameof(tarifa)} é obrigatório")]
        public Double tarifa { get; set; }

        [DisplayName("Gasto Mensal")]
        [Required(ErrorMessage = $"Campo {nameof(gastoMensal)} é obrigatório")]
        public Double gastoMensal { get; set; }

        [DisplayName("Economia")]
        [Required(ErrorMessage = $"Campo {nameof(economia)} é obrigatório")]
        public Double economia { get; set; }

        [DisplayName("Usuario")]
        [Required(ErrorMessage = $"Campo {nameof(fk_usuario)} é obrigatório")]
        public int fk_usuario { get; set; }
    }
}
