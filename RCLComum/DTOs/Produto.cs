using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RCLComum.DTOs
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Detalhe { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public decimal Stock { get; set; }
        public bool Visivel { get; set; } // Equivalente ao "Estado" ou "ParaVenda"
        public int CategoriaId { get; set; }
        // Mantemos o array de bytes que vem da BD
        public byte[]? Imagem { get; set; }
  public string ImagemUrl
        {
            get
            {
                if (Imagem == null || Imagem.Length == 0)
                    return "/img/sem_imagem.png"; // Uma imagem default na pasta wwwroot/img

                string base64 = Convert.ToBase64String(Imagem);
                return $"data:image/png;base64,{base64}";
            }
        }

        // Se precisares de mostrar o nome da Categoria, adiciona aqui
        // O API terá de preencher isto antes de enviar
        public Categoria? Categoria { get; set; }
        public string? CategoriaNome { get; set; }
    }
}