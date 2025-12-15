using GestaoLoja.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoLoja.Entities
{
    public class Encomenda
    {
        public int Id { get; set; }

        public DateTime Data { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorTotal { get; set; }

        [StringLength(50)]
        public string Estado { get; set; } = "Pendente";

        [Required]
        public string ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public virtual ApplicationUser? Cliente { get; set; }

        public virtual ICollection<DetalheEncomenda>? Detalhes { get; set; }
    }
}