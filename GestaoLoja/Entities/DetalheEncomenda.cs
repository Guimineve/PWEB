using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoLoja.Entities
{
    public class DetalheEncomenda
    {
        public int Id { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecoUnitario { get; set; }

        public int EncomendaId { get; set; }
        public virtual Encomenda? Encomenda { get; set; }

        public int ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }
    }
}