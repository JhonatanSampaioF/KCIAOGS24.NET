using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCIAOGS24.NET.Domain.Entities
{
    [Table("tb_usuario")]
    public class UsuarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Id")]
        public int id {  get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("Email")]
        public string email { get; set; }

        public ICollection<EnderecoEntity> Enderecos { get; set; } = new List<EnderecoEntity>();
    }
}
