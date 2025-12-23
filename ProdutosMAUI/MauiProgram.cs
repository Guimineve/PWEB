using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using RCLComum.Interfaces;
using RCLComum.Services;
namespace ProdutosMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CustomAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthStateProvider>());

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
            string enderecoApi;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                // Endereço especial para Android Emulator falar com o PC
                // MUDA A PORTA "7000" PARA A PORTA REAL DA TUA API (HTTPS)
                enderecoApi = "https://pf1r44cp-7135.uks1.devtunnels.ms";
            }
            else
            {
                // Para Windows Machine, iOS ou Browser
                enderecoApi = "https://pf1r44cp-7135.uks1.devtunnels.ms";
            }

            // O Erro "System.Net.Http.HttpClient" resolve-se com esta linha:
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(enderecoApi) });
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<IApiServices, ApiServices>();
            builder.Services.AddScoped<ICardsUtilsServices, CardsUtilsServices>();
            builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();



#endif

            return builder.Build();
        }
    }
}
