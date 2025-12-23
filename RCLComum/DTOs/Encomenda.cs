using System;
using System.Collections.Generic;

namespace RCLComum.DTOs
{
    public class Encomenda
    {
        public int Id { get; set; }
        public DateTime DataEncomenda { get; set; }
        public decimal ValorTotal { get; set; }
        public string Estado { get; set; } = "Pendente"; // Ex: Pendente, Enviado, Entregue

        // Uma encomenda tem uma lista de produtos lá dentro
        public List<DetalheEncomenda> Detalhes { get; set; } = new();
    }

    public class DetalheEncomenda
    {
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; } = string.Empty;
        public string ImagemUrl { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public decimal TotalLinha => Quantidade * PrecoUnitario;
    }
}