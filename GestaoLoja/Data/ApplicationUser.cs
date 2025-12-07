using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoLoja.Data
{    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public int NIF { get; set; }

        public byte[]? Fotografia { get; set; }

        [NotMapped]
        public string? ImageFile { get; set; }
    }
}