using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.DTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string UtilizadorId { get; set; }
        public string UtilizadorNome { get; set; }
    }
}
