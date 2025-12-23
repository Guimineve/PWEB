using RESTfulAPI.Entities;

namespace RESTfulAPI.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> GetAllProdutosAsync();
        Task<Produto?> GetProdutoByIdAsync(int id);
        Task<Produto> AddProdutoAsync(Produto produto);
        Task<Produto?> UpdateProdutoAsync(Produto produto);
        Task<bool> DeleteProdutoAsync(int id);
        Task<bool> ProdutoExistsAsync(int id);
    }
}
