using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RCLAPI.DTO;

namespace RCLAPI.Interfaces;

public interface ICarrinhoService
{
    // Evento para avisar as páginas que o carrinho mudou (ex: atualizar o ícone do cesto)
    event Action OnCarrinhoChanged;

    Task AdicionarItem(ItemCarrinho item);
    Task RemoverItem(int produtoId);
    Task<List<ItemCarrinho>> GetItens();
    Task LimparCarrinho();
    Task<int> GetTotalItens();
    Task<decimal> GetPrecoTotal();
}