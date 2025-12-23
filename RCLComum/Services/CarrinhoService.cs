using RCLComum.DTOs;
using RCLComum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        // Lista privada onde guardamos as coisas
        private List<CarrinhoItem> _itens = new List<CarrinhoItem>();

        public List<CarrinhoItem> Itens => _itens;

        // Calcula o total somando tudo
        public decimal ValorTotal => _itens.Sum(i => i.Total);

        // Conta quantos produtos (quantidade total) temos
        public int ContagemItens => _itens.Sum(i => i.Quantidade);

        // O Evento
        public event Action? OnChange;

        public void AdicionarProduto(Produto produto)
        {
            // Verifica se já existe no carrinho
            var itemExistente = _itens.FirstOrDefault(i => i.Produto.Id == produto.Id);

            if (itemExistente != null)
            {
                // Se já existe, só aumenta a quantidade
                itemExistente.Quantidade++;
            }
            else
            {
                // Se não existe, adiciona novo
                _itens.Add(new CarrinhoItem { Produto = produto, Quantidade = 1 });
            }

            NotificarMudanca();
        }

        public void RemoverProduto(Produto produto)
        {
            var item = _itens.FirstOrDefault(i => i.Produto.Id == produto.Id);
            if (item != null)
            {
                _itens.Remove(item);
                NotificarMudanca();
            }
        }

        public void LimparCarrinho()
        {
            _itens.Clear();
            NotificarMudanca();
        }

        private void NotificarMudanca()
        {
            OnChange?.Invoke();
        }
        public void AumentarQuantidade(Produto produto)
        {
            var item = _itens.FirstOrDefault(i => i.Produto.Id == produto.Id);
            if (item != null)
            {
                item.Quantidade++;
                NotificarMudanca();
            }
        }

        public void DiminuirQuantidade(Produto produto)
        {
            var item = _itens.FirstOrDefault(i => i.Produto.Id == produto.Id);
            if (item != null)
            {
                if (item.Quantidade > 1)
                {
                    item.Quantidade--;
                }
                else
                {
                    _itens.Remove(item);
                }
                NotificarMudanca();
            }
        }
    }
}
