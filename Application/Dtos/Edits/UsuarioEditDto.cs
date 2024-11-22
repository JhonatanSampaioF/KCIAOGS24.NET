using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KCIAOGS24.NET.Application.Dtos.Edits
{
    public class UsuarioEditDto
    {
        [DisplayName("Id")]
        [Required(ErrorMessage = $"Campo {nameof(id)} é obrigatório")]
        public int id { get; set; }

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
