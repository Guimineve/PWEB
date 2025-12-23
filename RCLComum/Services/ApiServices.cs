using RCLComum.DTOs;
using RCLComum.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
// Importa System.Text.Json para ler as respostas
using System.Text.Json;
using System.Threading.Tasks;

namespace RCLComum.Services
{
    public class ApiServices : IApiServices
    {
        private readonly HttpClient _httpClient;

        public ApiServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<bool>> Login(LoginModel model)
        {
            try
            {
                // 1. Envia o POST para a API
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", model);

                // 2. Prepara a resposta
                var apiResponse = new ApiResponse<bool>();

                if (response.IsSuccessStatusCode)
                {
                    // Se correu bem (200 OK)
                    //TODO: Mais tarde vamos precisar de ler o TOKEN aqui.
                    // Por agora, assumimos apenas que o login foi válido.

                    apiResponse.Sucesso = true;
                    apiResponse.Data = true;
                    apiResponse.Mensagem = "Login efetuado com sucesso.";
                }
                else
                {
                    // Se correu mal (400 ou 401), tentamos ler a mensagem de erro da API
                    var errorContent = await response.Content.ReadAsStringAsync();
                    apiResponse.Sucesso = false;
                    apiResponse.Data = false;
                    apiResponse.Mensagem = !string.IsNullOrEmpty(errorContent) ? errorContent : "Credenciais inválidas.";
                }

                return apiResponse;
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Sucesso = false,
                    Data = false,
                    Mensagem = $"Erro de comunicação: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<bool>> RegistarUtilizador(RegisterModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Register", model);

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<bool> { Sucesso = true, Data = true, Mensagem = "Registo criado." };
                }
                else
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    return new ApiResponse<bool> { Sucesso = false, Data = false, Mensagem = msg };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public async Task<ApiResponse<List<Produto>>> GetProdutosAsync()
        {
            var resposta = new ApiResponse<List<Produto>>();

            try
            {
                // 1. Faz o pedido à API
                // Nota: Ajusta o URL "api/Produtos" se o teu controller tiver outro nome
                var httpResponse = await _httpClient.GetAsync("api/Produtos");

                if (httpResponse.IsSuccessStatusCode)
                {
                    // 2. Se correu bem, lemos a lista e metemos no envelope
                    var lista = await httpResponse.Content.ReadFromJsonAsync<List<Produto>>();

                    resposta.Sucesso = true;
                    resposta.Data = lista;
                    resposta.Mensagem = "Produtos carregados com sucesso.";
                }
                else
                {
                    // 3. Se a API deu erro (Ex: 404, 500)
                    resposta.Sucesso = false;
                    resposta.Data = new List<Produto>();
                    resposta.Mensagem = $"Erro da API: {httpResponse.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                // 4. Se a ligação falhou (API desligada, sem net)
                resposta.Sucesso = false;
                resposta.Data = new List<Produto>();
                resposta.Mensagem = $"Erro de comunicação: {ex.Message}";
            }

            return resposta;
        }
    }
}