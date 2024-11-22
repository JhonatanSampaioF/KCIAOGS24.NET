using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCIAOGS24.NET.Domain.Entities
{
    [Table("tb_endereco")]
    public class EnderecoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Id")]
        public int id { get; set; }

        [Required]
        [DisplayName("Tipo Residencial")]
        public string tipoResidencial { get; set; }
        
        [Required]
        [DisplayName("Nome")]
        public string nome { get; set; }

        [Required]
        [DisplayName("CEP")]
        public string cep { get; set; }

        [Required]
        [DisplayName("Tarifa")]
        public Double tarifa { get; set; }

        [Required]
        [DisplayName("Gasto Mensal")]
        public Double gastoMensal { get; set; }

        [Required]
        [DisplayName("Economia")]
        public Double economia { get; set; }

        [ForeignKey("Usuario")]
        [DisplayName("Usuario")]
        public int fk_usuario { get; set; }

        public UsuarioEntity Usuario { get; set; }

        public EnergiaSolarEntity EnergiaSolar { get; set; }

        public EnergiaEolicaEntity EnergiaEolica { get; set; }
    }
}
