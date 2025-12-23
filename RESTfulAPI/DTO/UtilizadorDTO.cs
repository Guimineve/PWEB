
using System.ComponentModel.DataAnnotations;

namespace RESTfulAPI.DTO;
public class UtilizadorDTO
{
    public string Email { get; set; }
    [Required]
    [StringLength(100)]
    public string? Nome { get; set; }
    [Required]
    [StringLength(100)]
    public string? Morada { get; set; }
    public string EstadoConta { get; set; }
    public long? NIF { get; set; }
}
