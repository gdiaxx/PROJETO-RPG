using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace RpgApi.Controllers

{
    [Authorize(Roles = "Jogador, Admin")]
    [ApiController]
    [Route("[controller]")]

    public class PersonagemController : ControllerBase
    {
        private readonly DataContext _context; // variavel
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PersonagemController(DataContext context, IHttpContextAccessor httpContextAccessor) //CAMINHO DO BANCO
        {
                _context = context;
                _httpContextAccessor = httpContextAccessor;

        }

        private int ObterUsuarioId() //retornar id do usuario
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        private string ObterPerfilUsuario() //buscar no token qual é o tipo de perfil
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

[HttpPost]
        public async Task<IActionResult> AddPersonagemAsync(Personagem novoPersonagem) //desafio 3 e add perso..
        {   
    
            novoPersonagem.Usuario = await _context.Usuarios.FirstOrDefaultAsync(uBusca => uBusca.Id == ObterUsuarioId());
            
             //salvamento de dados
            await _context.Personagens.AddAsync(novoPersonagem);
            await _context.SaveChangesAsync();
            
            List<Personagem> listaPersonagens = await _context.Personagens.ToListAsync();
            return Ok(listaPersonagens);
        }

    [HttpGet("GetAll")] //pegar todos personagens; recuperar

        public async Task<IActionResult> GetAsync()
        {
            List<Personagem> listaPersonagens = new List<Personagem>();
             
                if(ObterPerfilUsuario()== "Admin") //se for admin, mostrar tudo
                {
                    listaPersonagens = await _context.Personagens.ToListAsync();
                }
                else
                    listaPersonagens = await _context.Personagens //se for jogador, mostrar personagens criado por ele
                    .Where(p => p.Usuario.Id == ObterUsuarioId()).ToListAsync();
            
            return Ok(listaPersonagens);
        }

    [HttpPut]
        
        public async Task<IActionResult> UptadePersonagemAsync(Personagem p)
        {
            p.Usuario = await _context.Usuarios.FirstOrDefaultAsync(uBusca => uBusca.Id == ObterUsuarioId());
            _context.Personagens.Update(p);
           await _context.SaveChangesAsync();

            return Ok(p);
        }

    [HttpDelete("{id}")]
    
        public async Task<IActionResult> DeleteAsync(int id)
        {
           
             Personagem pRemover = await _context.Personagens.FirstOrDefaultAsync(p => p.Id == id);
             
             _context.Personagens.Remove(pRemover);
            await _context.SaveChangesAsync();

            List<Personagem> listaPersonagens = await _context.Personagens.ToListAsync();

            return Ok(listaPersonagens);
        }

    [HttpGet("{id}")] //metodo assincrono -- buscar personagem na api
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            Personagem p = await _context.Personagens
            .Include(ar => ar.Arma) //add includes
            .Include(ph => ph.PersonagemHabilidades).ThenInclude(h => h.Habilidade)
            .FirstOrDefaultAsync(b => b.Id == id);
            
            return Ok(p);
        }
    
    [HttpGet("Desafio/{id}")] //desafio
    public async Task<IActionResult> GetSingleAsync2(int id){
        Personagem p = await _context.Personagens
        .Include(usu => usu.Usuario)
        .Include(arm => arm.Arma)
        .Include(ph => ph.PersonagemHabilidades).ThenInclude(h => h.Habilidade)
        .FirstOrDefaultAsync(b => b.Id == id);
        return Ok(p);
        } 

    [HttpPost("DeletePersonagemHabilidade")] //desafio 2 
     public async Task<IActionResult> DeletePersonagemHabilidade(PersonagemHabilidade ph){
         PersonagemHabilidade phRemove = await _context.PersonagemHabilidades
         .FirstOrDefaultAsync(phBusca => phBusca.PersonagemId == ph.PersonagemId && phBusca.HabilidadeId == ph.HabilidadeId);

         _context.PersonagemHabilidades.Remove(phRemove);
         await _context.SaveChangesAsync();

         return Ok(phRemove);
     }
    
    [HttpGet("GetByUser")] //metodo para recuperar quem esta fazendo uma requisicao, logando etc
    public async Task<IActionResult> GetByUserAsync()
    {
        int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        
        List<Personagem> personagens = await _context.Personagens.Where(c => c.Usuario.Id == id).ToListAsync();
        return Ok(personagens);
    }
    
    }

}