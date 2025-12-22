using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLAPI.DTO;

public class UtilizadorDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Apelido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Telemovel { get; set; }
    public string? Rua { get; set; }
    public string? Localidade { get; set; }
    public string? CodigoPostal { get; set; }
    public string? Cidade { get; set; }
    public string? Pais { get; set; }
    public int NIF { get; set; }
}
