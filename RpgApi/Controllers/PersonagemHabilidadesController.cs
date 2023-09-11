using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers {

[ApiController]
[Route("[controller]")]
public class PersonagemHabilidadesController : ControllerBase
{
    private readonly DataContext _context;
    public PersonagemHabilidadesController(DataContext context)
    {
        _context = context;
    }
        
[HttpPost]
    public async Task<IActionResult> AddPersonagemHabilidadeAsync(PersonagemHabilidade novoPersonagemHabilidade)
    {
        Personagem personagem = await _context.Personagens
        .Include(p => p.Arma)
        .Include(p => p.PersonagemHabilidades).ThenInclude(ps => ps.Habilidade)
        .FirstOrDefaultAsync(p => p.Id == novoPersonagemHabilidade.PersonagemId);

        if(personagem == null)
            return BadRequest("Personagem não encontrado para o Id informado.");

        //buscar via id personagem incluindo armas e habilidades

        Habilidade habilidade = await _context.Habilidades
        .FirstOrDefaultAsync(s => s.Id == novoPersonagemHabilidade.HabilidadeId);

        if(habilidade == null)
            return BadRequest("Habilidade não encontrada");

        //busca pela habilidade

        PersonagemHabilidade ph = new PersonagemHabilidade();
        ph.Personagem = personagem;
        ph.Habilidade = habilidade;

        await _context.PersonagemHabilidades.AddAsync(ph); //adc a instancia
        await _context.SaveChangesAsync();
        //salvar

        return Ok(ph);
             
        }

[HttpGet("{personagemId}")] //ele mostra via os ID's, os dados tambem tanto de personagem quanto habilidade
        public async Task<IActionResult> GetHabilidadesPersonagem(int personagemId)
        {
            List<PersonagemHabilidade> phLista = new List<PersonagemHabilidade>();                            

            phLista = await _context.PersonagemHabilidades
            .Include(p => p.Personagem)
            .Include(p => p.Habilidade)
            .Where(p => p.Personagem.Id == personagemId).ToListAsync();
 
            return Ok(phLista);            
        }
 
[HttpGet("GetHabilidades")] //listar todas habilidades
        public async Task<IActionResult> GetHabilidades()
        {
            List<Habilidade> habilidades = new List<Habilidade>();                           

            habilidades = await _context.Habilidades.ToListAsync();
 
            return Ok(habilidades);            
        }
    
    }
    
}