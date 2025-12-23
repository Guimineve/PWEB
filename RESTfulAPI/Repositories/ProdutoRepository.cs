using Microsoft.EntityFrameworkCore;
using RESTfulAPI.Data;
using RESTfulAPI.Entities;
using RESTfulAPI.Repositories.Interfaces;

namespace RESTfulAPI.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.ModoEntrega)
                .Include(p => p.Fornecedor)
                .ToListAsync();
        }

        public async Task<Produto?> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.ModoEntrega)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Produto> AddProdutoAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<Produto?> UpdateProdutoAsync(Produto produto)
        {
            var existe = await _context.Produtos.FindAsync(produto.Id);
            if (existe == null) return null;
            _context.Entry(existe).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ProdutoExistsAsync(int id)
        {
            return await _context.Produtos.AnyAsync(e => e.Id == id);
        }
    }
}
