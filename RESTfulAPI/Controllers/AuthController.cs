using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using RESTfulAPI.DTO;
using RESTfulAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class UtilizadoresController : ControllerBase
{
    private readonly IConfiguration _config;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UtilizadoresController(IConfiguration config, UserManager<ApplicationUser> 
        userManager, SignInManager<ApplicationUser> signInManager)
    {
        _config = config;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // User Register
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegistarUser([FromBody] RegisterModel utilizador)
    {
        var utilizadorExiste = await _userManager.Users.FirstOrDefaultAsync(u => u.Email 
        == utilizador.Email);

        if (utilizadorExiste != null)
        {
            return BadRequest("Já existe um utilizador com este email");
        }

        // Criar o novo utilizador
        var novoUtilizador = new ApplicationUser
        {
            UserName = utilizador.Email,
            Email = utilizador.Email,
            Nome = utilizador.Nome,
            NIF = utilizador.NIF,
            Morada = utilizador.Morada,
            EstadoConta = "Pendente",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        await _userManager.CreateAsync(novoUtilizador, utilizador.Password);
        await _userManager.AddToRoleAsync(novoUtilizador, "Cliente");

        return StatusCode(StatusCodes.Status201Created);
    }

    // User Login
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginUser([FromBody] UtilizadorLoginModel utilizador)
    {
        var utilizadorAtual = await _userManager.Users.FirstOrDefaultAsync(u =>
                                 u.Email == utilizador.Email);

        if (utilizadorAtual is null)
        {
            return NotFound("Utilizador não encontrado");
        }

        // ************ Logar com Identity
        var result = await _signInManager.PasswordSignInAsync(utilizador.Email, 
            utilizador.Password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var tempUser = await _userManager.FindByEmailAsync(utilizador.Email);
            var userRoles = await _userManager.GetRolesAsync(tempUser);

            // Regra de negócio: Apenas users ativos se podem logar no front end
            if (!(tempUser.EstadoConta == "Ativo"))
            {
                return Forbid("Utilizador não autorizado a fazer login.");
            }

            // Gerar e gravar o token JWT
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email , utilizador.Email),
                new Claim(ClaimTypes.NameIdentifier, tempUser.Id),
                new Claim(ClaimTypes.Role, userRoles[0]!)
            };

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new ObjectResult(new
            {
                accesstoken = jwt,
                tokentype = "bearer",
                utilizadorid = utilizadorAtual.Id,
                utilizadornome = utilizadorAtual.Nome
            });
        }
        else
        {
            return BadRequest("Erro: Login Inválido!");
        }
    }

    // User data
    [Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ObterDadosUtilizador()
    {
        var idDoToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(idDoToken))
        {
            return Unauthorized("O token não contém um ID válido.");
        }

        var utilizador = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == idDoToken);

        if (utilizador is null)
        {
            return NotFound("Utilizador não encontrado.");
        }

        var utilizadorDto = new UtilizadorDTO
        {
            Email = utilizador.Email,
            Nome = utilizador.Nome,
            Morada = utilizador.Morada,
            NIF = utilizador.NIF,
            EstadoConta = utilizador.EstadoConta,

        };

        return Ok(utilizadorDto);
    }

    // User models
    public class UtilizadorLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }
        public int NIF { get; set; }
        public string Password { get; set; }
    }
}
