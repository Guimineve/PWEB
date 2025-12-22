using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestaoLoja.Data;

namespace GestaoLoja.Entities
{
    public class CarrinhoCompras
    {
        public int Id { get; set; }

        [Required]
        public int Quantidade { get; set; }

        // Produto
        public int ProdutoId { get; set; }
        public virtual Produto? Produto { get; set; }

        // Cliente
        [Required]
        public string ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual ApplicationUser? Cliente { get; set; }
    }
}