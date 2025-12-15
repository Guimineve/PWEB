using GestaoLoja.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoLoja.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Detalhe { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Stock { get; set; }

        public byte[]? Imagem { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public bool Visivel { get; set; }

        public string EstadoAprovacao { get; set; } = "Pendente";

        [Required]
        public string Tipo { get; set; }

        // --- Relações ---

        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }

        public int ModoEntregaId { get; set; }
        public virtual ModoEntrega? ModoEntrega { get; set; }

        public string? FornecedorId { get; set; }
        [ForeignKey("FornecedorId")]
        public virtual ApplicationUser? Fornecedor { get; set; }
    }
}