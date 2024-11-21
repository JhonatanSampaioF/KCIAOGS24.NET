using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCIAOGS24.NET.Domain.Entities
{
    [Table("tb_energia_solar")]
    public class EnergiaSolarEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Id")]
        public int id {  get; set; }

        [DisplayName("Area Placa")]
        public int areaPlaca { get; set; }

        [DisplayName("Irradiacao Solar")]
        public Double irradiacaoSolar { get; set; }

        [DisplayName("Energia Estimada Gerada")]
        public Double energiaEstimadaGerada { get; set; }

        [ForeignKey("Endereco")]
        [DisplayName("Endereco")]
        public int fk_endereco { get; set; }

        public EnderecoEntity Endereco { get; set; }
    }
}
