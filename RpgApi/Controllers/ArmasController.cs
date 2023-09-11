using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using RpgApi.Models;
using RpgApi.Models.Enuns;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArmasController : ControllerBase
    {
         private readonly DataContext _context; // variavel
         private readonly IHttpContextAccessor _httpContextAccessor;
        public ArmasController(DataContext context, IHttpContextAccessor httpContextAccessor) //CAMINHO DO BANCO
        {
                _context = context;
                _httpContextAccessor = httpContextAccessor;

        }
          private int ObterUsuarioId() //retornar id do usuario
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

    [HttpGet("GetAll")] //pegar todos personagens; recuperar

        public async Task<IActionResult> GetAsync()
        {
            List<Arma> listaArmas = await _context.Armas.ToListAsync();
            return Ok(listaArmas);
        }

    [HttpPut("Put")]
        
        public async Task<IActionResult> UptadeArmaAsync(Arma a)
        {
            _context.Armas.Update(a);
           await _context.SaveChangesAsync();

            return Ok(a);
        }

    [HttpDelete("DeleteId/{id}")]
    
        public async Task<IActionResult> DeleteAsync(int id)
        {
           
             Arma aRemover = await _context.Armas.FirstOrDefaultAsync(a => a.Id == id);
             
             _context.Armas.Remove(aRemover);
            await _context.SaveChangesAsync();

            List<Arma> listaArmas = await _context.Armas.ToListAsync();

            return Ok(listaArmas);
        }

    [HttpGet("GetId/{id}")] //metodo assincrono -- buscar personagem na api
        public async Task<IActionResult> GetSingleAsync(int id)
        {
            Arma a = await _context.Armas.FirstOrDefaultAsync(a => a.Id == id);
            return Ok(a);
        }

    [HttpPost]
        public async Task<IActionResult> AddArmaAsync(Arma novaArma)
        {
            Personagem personagem = await _context.Personagens.FirstOrDefaultAsync(P => P.Id == novaArma.PersonagemId);
            
            if(personagem == null)
                return BadRequest("Não existe!");

           Arma arma = await _context.Armas.FirstOrDefaultAsync(a => a.PersonagemId == novaArma.PersonagemId);
           
            if(arma != null)
                return BadRequest("Já está vinculado!");

            await _context.Armas.AddAsync(novaArma);
            await _context.SaveChangesAsync();

            List<Arma> armas = await _context.Armas
            .Where(p => p.PersonagemId == novaArma.PersonagemId) 
            .ToListAsync();

            return Ok(armas);
        } 
    
    }
}