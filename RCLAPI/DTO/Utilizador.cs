using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLAPI.DTO;

public class Utilizador
{
    // O Blazor precisa destas propriedades para o formulário de registo
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O apelido é obrigatório")]
    public string Apelido { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required, Compare("Password", ErrorMessage = "As passwords não coincidem")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public int NIF { get; set; }
    public string Telemovel { get; set; } = string.Empty;
    public string Rua { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty;
    public string CodigoPostal { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
}