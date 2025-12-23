using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.DTOs
{
    public class CarrinhoItem
    {
        public Produto Produto { get; set; } = new();
        public int Quantidade { get; set; } = 1;

        // Propriedade calculada para facilitar a vida
        public decimal Total => Produto.Preco * Quantidade;
    }
}
