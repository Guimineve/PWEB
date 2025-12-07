using Microsoft.AspNetCore.Identity;

namespace GestaoLoja.Data
{
    public static class Initialization
    {
        public static async Task CriaDadosIniciais(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            //Adicionar as roles default
            string[] roles = new[] { "Admin", "Gestor", "Cliente" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    IdentityRole roleRole = new IdentityRole(role);
                    await roleManager.CreateAsync(roleRole);
                }
            }

            //Adicionar default user - Admin
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@localhost.com",
                Email = "admin@localhost.com",
                Nome = "Administrador",
                Apelido = "Local",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            //validação se o Id já existe
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null) //Tenta encontrar o email. Se não encontrar atribui a senha default e o role
                {
                    await userManager.CreateAsync(defaultUser, "Is3C..00");
                    await userManager.AddToRoleAsync(defaultUser, "Admin");
                }
            }
            
        }

    }
}
