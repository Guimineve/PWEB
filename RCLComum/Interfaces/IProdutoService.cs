using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCLComum.DTOs;
namespace RCLComum.Interfaces
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetProdutosAsync();

        Task<Produto> GetProdutoPorIdAsync(int id);

         Task CriarProdutoAsync(Produto produto);
         Task AtualizarProdutoAsync(Produto produto);



        // Para o destaque aleatório
        Task<Produto?> GetProdutoDestaqueAsync();
    }
    }
