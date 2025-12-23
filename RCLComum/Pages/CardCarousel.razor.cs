using Microsoft.AspNetCore.Components; 
using RCLComum.DTOs;
using RCLComum.Interfaces;
using RCLComum.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RCLComum.Pages 
{
    public partial class CardCarousel : ComponentBase
    {
        // --- INJEÇÃO DE DEPENDÊNCIAS ---
        // No ficheiro .razor usas @inject. Aqui no .cs tens de usar [Inject] public ...

        [Inject]
        public IApiServices ApiServices { get; set; } // Resolve o erro do ApiService

        [Inject]
        public ICardsUtilsServices cardsUtilsServices { get; set; } // Resolve o erro "cardsUtilsServices does not exist"

        // --- VARIÁVEIS LOCAIS ---
        private List<Produto>? Produtos { get; set; }
        private bool IsDisabledNext { get; set; } = false;
        private bool IsDisabledPrevious { get; set; } = false;

        // --- LÓGICA ---

        // Resolve o erro "no suitable method found to override"
        protected override async Task OnInitializedAsync()
        {
            // Resolve o erro "List<Produto> does not contain definition for Sucesso"
            // O truque é receber a resposta no objeto 'wrapper' primeiro
            var response = await ApiServices.GetProdutosAsync();

            if (response.Sucesso && response.Data != null)
            {
                Produtos = response.Data;
            }
            else
            {
                Produtos = new List<Produto>();
            }

            await LoadMarginsLeft();

            // Resolve o erro "StateHasChanged does not exist"
            cardsUtilsServices.OnChange += StateHasChanged;
        }

        async Task LoadMarginsLeft()
        {
            cardsUtilsServices.MarginLeftSlide = new List<string>();
            if (Produtos != null)
            {
                foreach (var p in Produtos)
                {
                    cardsUtilsServices.MarginLeftSlide.Add("margin-left:0%");
                }
            }
        }

        void PreviousCard()
        {
            if (cardsUtilsServices.CountSlide != 0)
            {
                cardsUtilsServices.MarginLeftSlide[cardsUtilsServices.CountSlide - 1] = "margin-left:0%";
                cardsUtilsServices.CountSlide--;
                IsDisabledNext = false;
                IsDisabledPrevious = false;
            }
            else
            {
                IsDisabledPrevious = true;
            }
            cardsUtilsServices.Index = cardsUtilsServices.CountSlide;
        }

        void NextCard()
        {
            cardsUtilsServices.CountSlide++;
            cardsUtilsServices.Index = cardsUtilsServices.CountSlide;

            if (cardsUtilsServices.CountSlide < cardsUtilsServices.MarginLeftSlide.Count)
            {
                // Ajusta este valor (-220px) à largura do teu cartão CSS
                cardsUtilsServices.MarginLeftSlide[cardsUtilsServices.CountSlide - 1] = "margin-left:-220px";
                IsDisabledNext = false;
                IsDisabledPrevious = false;
            }
            else
            {
                IsDisabledNext = true;
            }
        }

        public void Dispose()
        {
            cardsUtilsServices.OnChange -= StateHasChanged;
        }
    }
}