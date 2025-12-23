using ProdutosBlazor.Components;
using RCLComum.Services;
using RCLComum.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IApiServices, ApiServices>();
builder.Services.AddScoped<ICardsUtilsServices, CardsUtilsServices>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7000") 
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddAdditionalAssemblies([typeof(RCLComum.Pages.Home).Assembly])
    .AddInteractiveServerRenderMode();

app.Run();
