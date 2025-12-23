using RCLComum.DTOs;
using RCLComum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCLComum.Services
{
    public class ApiServicesMock : IApiServices
    {
        // --- DADOS FALSOS ---

        // 1. LOGIN
        public async Task<ApiResponse<bool>> Login(LoginModel model)
        {
            // Finge que demora 500ms a pensar
            await Task.Delay(500);

            // Aceita qualquer login para não te chatear nos testes
            return new ApiResponse<bool>
            {
                Sucesso = true,
                Data = true,
                Mensagem = "Login Mock com sucesso!"
            };
        }

        public async Task<ApiResponse<bool>> RegistarUtilizador(RegisterModel model)
        {
            await Task.Delay(500);
            return new ApiResponse<bool> { Sucesso = true, Data = true, Mensagem = "Utilizador registado (Mock)." };
        }

        // 2. CATEGORIAS (Para o Slider de Topo)
        // ... usings ...

        public async Task<ApiResponse<List<Categoria>>> GetCategoriasAsync()
        {
            var listaFalsa = new List<Categoria>
    {
        new Categoria { Id = 1, Nome = "Hambúrgueres" },
        new Categoria { Id = 2, Nome = "Bebidas" },
        new Categoria { Id = 3, Nome = "Sobremesas" }
    };
            // ... return response ...
            return await Task.FromResult(new ApiResponse<List<Categoria>> { Sucesso = true, Data = listaFalsa });
        }

        public async Task<ApiResponse<List<Produto>>> GetProdutosAsync()
        {
            var listaFalsa = new List<Produto>
    {
        // Categoria 1: Hambúrgueres
        new Produto
        {
            Id = 1, Nome = "X-Bacon", Preco = 8.50m,
            CategoriaId = 1, // <--- TEM DE SER 1
            ImagemUrl = "https://img.freepik.com/fotos-gratis/hamburguer-de-carne-com-salada-de-queijo-e-tomate-em-piso-escuro_140725-89524.jpg"
        },
        new Produto
        {
            Id = 2, Nome = "Cheeseburger", Preco = 10.00m,
            CategoriaId = 1, // <--- TEM DE SER 1
            ImagemUrl = "https://img.freepik.com/fotos-gratis/vista-frontal-do-hamburguer-de-carne-com-queijo-e-salada-na-frente-escuro_140725-89597.jpg"
        },

        // Categoria 2: Bebidas
        new Produto
        {
            Id = 3, Nome = "Cola Zero", Preco = 2.00m,
            CategoriaId = 2, // <--- TEM DE SER 2
            ImagemUrl = "https://img.freepik.com/fotos-gratis/bebida-de-refrigerante-gelada_144627-6330.jpg"
        },
        new Produto
        {
            Id = 4, Nome = "Limonada", Preco = 3.50m,
            CategoriaId = 2, // <--- TEM DE SER 2
            ImagemUrl = "https://img.freepik.com/fotos-gratis/limonada-gelada-com-fatias-de-limao_144627-26884.jpg"
        },
        
        // Categoria 3: Sobremesas
        new Produto
        {
            Id = 5, Nome = "Cheesecake", Preco = 4.50m,
            CategoriaId = 3, // <--- TEM DE SER 3
            ImagemUrl = "https://img.freepik.com/fotos-gratis/fatia-de-bolo-de-queijo-com-morangos_144627-16474.jpg"
        },
         new Produto
        {
            Id = 6, Nome = "Cheesecake", Preco = 4.50m,
            CategoriaId = 3, // <--- TEM DE SER 3
            ImagemUrl = "https://img.freepik.com/fotos-gratis/fatia-de-bolo-de-queijo-com-morangos_144627-16474.jpg"
        },
          new Produto
        {
            Id = 7, Nome = "Cheesecake", Preco = 4.50m,
            CategoriaId = 3, // <--- TEM DE SER 3
            ImagemUrl = "https://img.freepik.com/fotos-gratis/fatia-de-bolo-de-queijo-com-morangos_144627-16474.jpg"
        },
           new Produto
        {
            Id = 8, Nome = "Cheesecake", Preco = 4.50m,
            CategoriaId = 3, // <--- TEM DE SER 3
            ImagemUrl = "https://img.freepik.com/fotos-gratis/fatia-de-bolo-de-queijo-com-morangos_144627-16474.jpg"
        },
            new Produto
        {
            Id = 9, Nome = "Cheesecake", Preco = 4.50m,
            CategoriaId = 3, // <--- TEM DE SER 3
            ImagemUrl = "https://img.freepik.com/fotos-gratis/fatia-de-bolo-de-queijo-com-morangos_144627-16474.jpg"
        },
             new Produto
        {
            Id = 51, Nome = "Cheesecake", Preco = 4.50m,
            CategoriaId = 3, // <--- TEM DE SER 3
            ImagemUrl = "https://img.freepik.com/fotos-gratis/fatia-de-bolo-de-queijo-com-morangos_144627-16474.jpg"
        }
    };

            return await Task.FromResult(new ApiResponse<List<Produto>> { Sucesso = true, Data = listaFalsa });
        }

        // 4. ENCOMENDAS
        public async Task<ApiResponse<List<Encomenda>>> GetMinhasEncomendasAsync(int userId)
        {
            var lista = new List<Encomenda>
            {
                new Encomenda
                {
                    Id = 101, DataEncomenda = DateTime.Now.AddDays(-2), ValorTotal = 12.50m, Estado = "Entregue",
                    Detalhes = new List<DetalheEncomenda>
                    {
                        new DetalheEncomenda { ProdutoNome = "X-Bacon", Quantidade = 1, PrecoUnitario = 8.50m },
                        new DetalheEncomenda { ProdutoNome = "Cola Zero", Quantidade = 2, PrecoUnitario = 2.00m }
                    }
                },
                new Encomenda
                {
                    Id = 102, DataEncomenda = DateTime.Now, ValorTotal = 4.50m, Estado = "Pendente",
                    Detalhes = new List<DetalheEncomenda>
                    {
                        new DetalheEncomenda { ProdutoNome = "Cheesecake", Quantidade = 1, PrecoUnitario = 4.50m }
                    }
                }
            };

            return await Task.FromResult(new ApiResponse<List<Encomenda>> { Sucesso = true, Data = lista });
        }

        // Método fake para checkout
        public async Task<ApiResponse<bool>> CriarEncomendaAsync(Encomenda novaEncomenda)
        {
            await Task.Delay(1000); // Simula tempo de rede
            return new ApiResponse<bool> { Sucesso = true, Mensagem = "Encomenda simulada com sucesso!" };
        }
        public async Task<ApiResponse<Produto>> GetProdutoByIdAsync(int id)
        {
            // Vamos buscar a lista completa (simulando a base de dados)
            var todos = await GetProdutosAsync();

            // Procura o produto com aquele ID
            var produto = todos.Data?.FirstOrDefault(p => p.Id == id);

            if (produto != null)
            {
                return new ApiResponse<Produto> { Sucesso = true, Data = produto };
            }

            return new ApiResponse<Produto> { Sucesso = false, Mensagem = "Produto não encontrado." };
        }
    }
}