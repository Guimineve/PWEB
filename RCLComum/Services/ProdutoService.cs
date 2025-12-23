using RCLComum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using RCLComum.DTOs;
namespace RCLComum.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _httpClient;

        public ProdutoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Produto>> GetProdutosAsync()
        {
            // Tenta obter os dados. 
            // "api/produtos" deve bater certo com o [Route] do teu Controller na API
            var resposta = await _httpClient.GetFromJsonAsync<List<Produto>>("api/produtos");
            return resposta ?? new List<Produto>();
        }

        public async Task<Produto> GetProdutoPorIdAsync(int id)
        {
            var resposta = await _httpClient.GetFromJsonAsync<Produto>($"api/produtos/{id}");

            if (resposta == null)
                throw new Exception("Produto não encontrado.");

            return resposta;
        }
        public async Task CriarProdutoAsync(Produto produto)
        {
            // POST: api/produtos
            await _httpClient.PostAsJsonAsync("api/produtos", produto);
        }

        public async Task AtualizarProdutoAsync(Produto produto)
        {
            // PUT: api/produtos/5
            await _httpClient.PutAsJsonAsync($"api/produtos/{produto.Id}", produto);
        }




        public async Task<Produto?> GetProdutoDestaqueAsync()
        {
            // OPÇÃO A: Se a API não tiver um endpoint "random",
            // pedimos todos e escolhemos um aqui (ok para projetos pequenos).
            var todos = await GetProdutosAsync();

            if (todos == null || !todos.Any())
                return null;

            var random = new Random();
            int index = random.Next(todos.Count);

            return todos[index];
        }
    }
}
