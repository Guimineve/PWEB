using GestaoLoja.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoLoja.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Detalhe { get; set; }
        public string Origem { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal EmStock { get; set; }

        public bool MaisVendido { get; set; }
        public bool Disponivel { get; set; }
        public bool Promocao { get; set; }

        public string? UrlImagem { get; set; }
        public byte[]? Imagem { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? categoria { get; set; }

        public int ModoEntregaId { get; set; }
        public ModoEntrega? modoentrega { get; set; }

        public string EstadoAprovacao { get; set; } = "Pendente";

        public string? FornecedorId { get; set; }

        [ForeignKey("FornecedorId")]
        public ApplicationUser? Fornecedor { get; set; }
    }
}