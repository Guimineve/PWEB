using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestaoLoja.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [StringLength(100)]
        public string Nome { get; set; }

        [PersonalData]
        public int NIF { get; set; }

        [PersonalData]
        [StringLength(200)]
        public string Morada { get; set; }

        public string EstadoConta { get; set; } = "Pendente";
    }
}