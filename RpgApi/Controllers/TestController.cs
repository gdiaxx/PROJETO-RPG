using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() {Id = 1, Nome = "Sam", PontosVida = 100, Forca = 20 , Inteligencia = 20, Defesa = 10, Classe=ClasseEnum.Clerigo},
            new Personagem() {Id = 2, Nome = "Frodo", PontosVida = 100, Forca = 10, Inteligencia = 40, Defesa = 15, Classe=ClasseEnum.Mago},
            new Personagem() {Id = 3, Nome = "Mario", PontosVida = 100, Forca = 10 , Inteligencia = 20, Defesa = 20, Classe=ClasseEnum.Cavaleiro},
            new Personagem() {Id = 4, Nome = "Gandalf", PontosVida = 100, Forca = 30 , Inteligencia = 20, Defesa = 30, Classe=ClasseEnum.Clerigo},
            new Personagem() {Id = 5, Nome = "Hobbit", PontosVida = 100, Forca = 20 , Inteligencia = 20, Defesa = 30, Classe=ClasseEnum.Mago},
            new Personagem() {Id = 6, Nome = "Celeborn", PontosVida = 100, Forca = 40 , Inteligencia = 20, Defesa = 30, Classe=ClasseEnum.Mago},
            new Personagem() {Id = 7, Nome = "Ragadast", PontosVida = 100, Forca = 20 , Inteligencia = 20, Defesa = 10, Classe=ClasseEnum.Cavaleiro}
        }; 

[HttpGet("GetAll")]
            public IActionResult Get()
        {
            return Ok(personagens);
        }

        public IActionResult GetSingle()
        {
            return Ok(personagens[7]);
        }

    [HttpGet("{id}")]
         public IActionResult GetById(int id)
        {
            Personagem pEncontrado = personagens.FirstOrDefault(p => p.Id == id);
            return Ok(pEncontrado);
            //return Ok(personagens.FirstOrDefault(p => p.Id == id));
        }

        [HttpGet("GetCombinacao")]
        public IActionResult GetCombinacao(int id)
        {
                 List<Personagem> listaEncontrada = personagens.FindAll(p => p.Forca < 20 && p.Inteligencia > 30);
                return Ok(listaEncontrada);
        }

        [HttpGet("GetOrdenado")]
        public IActionResult GetOrdem(){
            List<Personagem> listaOrdenada = personagens.OrderBy(p => p.Forca).ToList();
            return Ok(listaOrdenada);
        }
        
        [HttpGet("GetContagem")]
        public IActionResult GetQuantidade()
        {
            return Ok("Quantidade de personagens: " + personagens.Count);
        }
        
        [HttpGet("GetSomaForca")]
        public IActionResult GetSomaForca()
        {
            return Ok(personagens.Sum(p => p.Forca));
        }

        [HttpGet("GetSemCavaleiro")]
        public IActionResult GetSemCavaleiro(){
            List<Personagem> listaBusca = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro);
            return Ok(listaBusca);
        }

         [HttpGet("GetByNomeAproximado/{nome}")]
        public IActionResult GetByNomeAproximado(string nome)
        {
            List<Personagem> listaBusca = personagens.FindAll(P => P.Nome.Contains(nome));
            return Ok(listaBusca);
        }

          [HttpGet("GetRemovendoMago")]
        public IActionResult GetRemovendoMago()
        {
           personagens.RemoveAll(p => p.Classe == ClasseEnum.Mago);
            return Ok(personagens);
        }

             [HttpGet("GetByInteligencia/{valor}")]
        public IActionResult GetByInteligencia(int valor)
        {
           List<Personagem> listaBusca = personagens.FindAll(p => p.Inteligencia == valor );
            if(listaBusca.Count == 0){
                return BadRequest("Nenhum personagem encontrado");
            }
            else
            {
                return Ok(listaBusca);
            }
        }
        [HttpPost]
        public IActionResult AddPersonagem(Personagem NovoPersonagem)
        {
            
            if(NovoPersonagem.Inteligencia == 0)
            return BadRequest("O valor da inteligencia nao pode ser igual a zero");
            
            personagens.Add(NovoPersonagem);
            return Ok(personagens);
        }
        [HttpPut]
        public IActionResult UptadePersonagem(Personagem p)
        {
            Personagem personagemAlterado = personagens.Find(pers => pers.Id == p.Id);
            personagemAlterado.Nome = p.Nome;
            personagemAlterado.PontosVida = p.PontosVida;
            personagemAlterado.Forca = p.Forca;
            personagemAlterado.Defesa = p.Defesa;
            personagemAlterado.Inteligencia = p.Inteligencia;
            personagemAlterado.Classe = p.Classe;        

            return Ok(personagens);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            personagens.RemoveAll(pers => pers.Id == id);
            return Ok(personagens);
        }
    }
}