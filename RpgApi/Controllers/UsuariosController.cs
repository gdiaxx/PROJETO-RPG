using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")] //nome da controller como rota
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public UsuariosController(DataContext context, IConfiguration configuration)
        {                                   
            _context = context;             
            _configuration = configuration;        //base para api's
        }

        private void CriarPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; //metodo para criar salt e hash para criptografar senha
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UsuarioExistente(string username) //metodo para ver se o usuario existe
        {
            if(await _context.Usuarios.AnyAsync(x => x.username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false; 
        }

        [AllowAnonymous]
        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(Usuario user)
        {
            if(await UsuarioExistente(user.username))
                return BadRequest("Nome de usuario ja existe!");

            CriarPasswordHash(user.PasswordString, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordString = string.Empty;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Usuarios.AddAsync(user); //await faz acontecer de forma assincrona, nao sai enquanto n retornar o resultado
            await _context.SaveChangesAsync();

            return Ok(user.Id);
        }
        private bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        } //verificar se a senha bate com o hash e o salt

        [AllowAnonymous]
        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciaisUsuario) //autenticar usuario, ou seja, se o usuario existe ou se a senha esta correta
        {
            Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.username.ToLower().Equals(credenciaisUsuario.username.ToLower()));
            if(usuario == null)
            {
                return BadRequest("Usuario não encontrado.");
            }
            else if (!VerificarPasswordHash(credenciaisUsuario.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
            {
                return BadRequest("Senha incorreta");
            }
            else
            {
                //return Ok(usuario.Id);
                return Ok(CriarToken(usuario)); //CRIAR TOKEN PARA CADA USUARIOS
            }
        }

        private string CriarToken(Usuario usuario) 
        { 
            List<Claim> claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()), 
                new Claim(ClaimTypes.Name, usuario.username), 
                new Claim(ClaimTypes.Role, usuario.Perfil)
            }; 
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8 
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value)); //chamar no appsetings
                
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); //tipo de seg 
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor 
                    { 
                    Subject = new ClaimsIdentity(claims), 
                    Expires = DateTime.Now.AddDays(1),          //SEGURANÇA DO TOKEN(EXPIRAÇÃO, ETC.)
                    SigningCredentials = creds 
                    }; 
                    
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler(); 
                    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor); 
                    return tokenHandler.WriteToken(token); }
    }
}