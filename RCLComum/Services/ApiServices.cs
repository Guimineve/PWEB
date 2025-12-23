using RCLComum.DTOs;
using RCLComum.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RCLComum.Services
{
    public class ApiServices : IApiServices
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _js;

        public ApiServices(HttpClient httpClient, IJSRuntime js)
        {
            _httpClient = httpClient;
            _js = js;
        }

        public async Task<ApiResponse<bool>> Login(LoginModel model)
        {
            try
            {
                // Envia o POST para a API
                var response = await _httpClient.PostAsJsonAsync("api/Utilizadores/LoginUser", model);

                // Prepara a resposta
                var apiResponse = new ApiResponse<bool>();

                if (response.IsSuccessStatusCode)
                {
                    var loginResult = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    if (loginResult != null && !string.IsNullOrEmpty(loginResult.AccessToken))
                    {
                        // GUARDAR O TOKEN NO BROWSER (localStorage)
                        await _js.InvokeVoidAsync("localStorage.setItem", "authToken", loginResult.AccessToken);

                        apiResponse.Sucesso = true;
                        apiResponse.Data = true;
                        apiResponse.Mensagem = "Login efetuado com sucesso.";
                    }
                    else
                    {
                        apiResponse.Sucesso = false;
                        apiResponse.Mensagem = "Token não recebido.";
                    }
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
        public async Task<ApiResponse<List<Categoria>>> GetCategoriasAsync()
        {
            var resposta = new ApiResponse<List<Categoria>>();
            try
            {
                var httpResponse = await _httpClient.GetAsync("api/Categorias"); 
                if (httpResponse.IsSuccessStatusCode)
                {
                    resposta.Sucesso = true;
                    resposta.Data = await httpResponse.Content.ReadFromJsonAsync<List<Categoria>>();
                }
                else
                {
                    resposta.Sucesso = false;
                    resposta.Data = new List<Categoria>();
                }
            }
            catch (Exception ex)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = ex.Message;
            }
            return resposta;
        }
        

        public async Task<ApiResponse<List<Encomenda>>> GetMinhasEncomendasAsync(int userId)
        {
            var resposta = new ApiResponse<List<Encomenda>>();
            try
            {
            
                var httpResponse = await _httpClient.GetAsync($"api/Encomendas/User/{userId}");

                if (httpResponse.IsSuccessStatusCode)
                {
                    resposta.Sucesso = true;
                    resposta.Data = await httpResponse.Content.ReadFromJsonAsync<List<Encomenda>>();
                }
                else
                {
                    resposta.Sucesso = false;
                    resposta.Data = new List<Encomenda>(); 
                    resposta.Mensagem = "Não foi possível carregar as encomendas.";
                }
            }
            catch (Exception ex)
            {
                resposta.Sucesso = false;
                resposta.Data = new List<Encomenda>();
                resposta.Mensagem = ex.Message;
            }
            return resposta;
        }

        public async Task<ApiResponse<bool>> CriarEncomendaAsync(Encomenda novaEncomenda)
        {
            // Exemplo rápido para criar encomenda
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Encomendas", novaEncomenda);
                return new ApiResponse<bool>
                {
                    Sucesso = response.IsSuccessStatusCode,
                    Data = response.IsSuccessStatusCode,
                    Mensagem = response.IsSuccessStatusCode ? "Sucesso" : "Erro ao gravar"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Sucesso = false, Mensagem = ex.Message };
            }
        }
    }
}