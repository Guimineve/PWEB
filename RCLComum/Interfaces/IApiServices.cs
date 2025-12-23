using RCLComum.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Interfaces
{
    public interface IApiServices
    {
        // O método de Login
        Task<ApiResponse<bool>> Login(LoginModel model);

        // Já que estamos aqui, deixamos preparado o do Registo também
        Task<ApiResponse<bool>> RegistarUtilizador(RegisterModel model);

        Task<ApiResponse<List<Produto>>> GetProdutosAsync();

        Task<ApiResponse<List<Categoria>>> GetCategoriasAsync();

        Task<ApiResponse<List<Encomenda>>> GetMinhasEncomendasAsync(int userId);
        Task<ApiResponse<bool>> CriarEncomendaAsync(Encomenda novaEncomenda);
    }
}
