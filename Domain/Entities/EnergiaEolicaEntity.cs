using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCIAOGS24.NET.Domain.Entities
{
    [Table("tb_energia_eolica")]
    public class EnergiaEolicaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Id")]
        public int id { get; set; }

        [DisplayName("Potencial Nominal")]
        public Double potencialNominal { get; set; }

        [DisplayName("Altura Torre")]
        public Double alturaTorre { get; set; }

        [DisplayName("Diametro Rotor")]
        public Double diametroRotor { get; set; }

        [DisplayName("Energia Estimada Gerada")]
        public Double energiaEstimadaGerada { get; set; }

        [ForeignKey("Endereco")]
        [DisplayName("Endereco")]
        public int fk_endereco { get; set; }

        public EnderecoEntity Endereco { get; set; }
    }
}
