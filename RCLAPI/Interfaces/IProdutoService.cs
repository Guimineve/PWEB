using RCLAPI.DTO;
// Ficheiro: RCLAPI/Interfaces/IProdutoService.cs (ou na pasta que definires)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLAPI.Interfaces;

public interface IProdutoService
{
    // Obtém todos os produtos da API
    Task<IEnumerable<ProdutoDTO>> GetProdutosAsync();

    // Obtém um produto específico pelo ID
    Task<ProdutoDTO?> GetProdutoByIdAsync(int id);

    [cite_start]// Obtém as categorias para preencher filtros ou menus 
    Task<IEnumerable<CategoriaDTO>> GetCategoriasAsync();

    // Obtém produtos de uma categoria específica
    Task<IEnumerable<ProdutoDTO>> GetProdutosPorCategoriaAsync(int categoriaId);
}