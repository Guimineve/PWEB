using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLAPI.DTO
{
    public class EncomendaDTO
    {
        public int Id { get; set; }
        public DateTime DataEncomenda { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemCarrinhoDTO> Itens { get; set; } = new();
    }
}
