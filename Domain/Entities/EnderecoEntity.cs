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

        [DisplayName("Tipo Residencial")]
        public string tipoResidencial { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }

        [DisplayName("CEP")]
        public string cep { get; set; }

        [DisplayName("Tarifa")]
        public Double tarifa { get; set; }

        [DisplayName("Gasto Mensal")]
        public Double gastoMensal { get; set; }

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
