using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfulAPI.Entities
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public byte[]? Imagem { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public int? CategoriaPaiId { get; set; }
        public virtual Categoria? CategoriaPai { get; set; }

        public virtual ICollection<Categoria>? SubCategorias { get; set; }

        public virtual ICollection<Produto>? Produtos { get; set; }
    }
}