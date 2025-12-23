using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.DTOs
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        // Mantemos o array de bytes caso venha da API
        public byte[]? Imagem { get; set; }

        // Propriedade auxiliar para mostrar a imagem no HTML
        public string ImagemUrl
        {
            get
            {
                if (Imagem == null || Imagem.Length == 0)
                    return "/img/sem_imagem_cat.png"; // Podes criar uma imagem default diferente se quiseres

                string base64 = Convert.ToBase64String(Imagem);
                return $"data:image/png;base64,{base64}";
            }
        }
    }
}