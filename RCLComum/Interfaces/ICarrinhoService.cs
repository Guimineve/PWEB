using RCLComum.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Interfaces
{
    public interface ICarrinhoService
    {
        List<CarrinhoItem> Itens { get; }
        decimal ValorTotal { get; }
        int ContagemItens { get; }

        void AdicionarProduto(Produto produto);
        void RemoverProduto(Produto produto);
        void LimparCarrinho();
        void AumentarQuantidade(Produto produto);
        void DiminuirQuantidade(Produto produto);

        // O evento mágico que avisa os componentes para atualizarem
        event Action OnChange;
    }
}
