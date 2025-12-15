using System.ComponentModel.DataAnnotations;

namespace GestaoLoja.Entities
{
    public class ModoEntrega
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(200)]
        public string? Detalhe { get; set; }

        public virtual ICollection<Produto>? Produtos { get; set; }
    }
}