using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KCIAOGS24.NET.Application.Dtos.Create
{
    public class UsuarioDto
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = $"Campo {nameof(nome)} é obrigatório")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Campo deve ter no mínimo 3 caracteres")]
        public string nome { get; set; } = string.Empty;

        [DisplayName("Email")]
        [Required(ErrorMessage = $"Campo {nameof(email)} é obrigatório")]
        [EmailAddress]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Campo deve ter no mínimo 3 caracteres")]
        public string email { get; set; } = string.Empty;
    }
}
