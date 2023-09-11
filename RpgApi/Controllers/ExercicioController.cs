using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            new Personagem() {Id = 1, Nome = "Sam", PontosVida = 100, Forca = 20 , Inteligencia = 20, Defesa = 10, Classe=ClasseEnum.Clerigo},
            new Personagem() {Id = 2, Nome = "Frodo", PontosVida = 90, Forca = 10, Inteligencia = 40, Defesa = 15, Classe=ClasseEnum.Mago},
            new Personagem() {Id = 3, Nome = "Mario", PontosVida = 100, Forca = 10 , Inteligencia = 20, Defesa = 20, Classe=ClasseEnum.Cavaleiro},
            new Personagem() {Id = 4, Nome = "Gandalf", PontosVida = 100, Forca = 30 , Inteligencia = 20, Defesa = 30, Classe=ClasseEnum.Clerigo},
            new Personagem() {Id = 5, Nome = "Hobbit", PontosVida = 100, Forca = 20 , Inteligencia = 20, Defesa = 30, Classe=ClasseEnum.Mago},
            new Personagem() {Id = 6, Nome = "Celeborn", PontosVida = 100, Forca = 40 , Inteligencia = 20, Defesa = 30, Classe=ClasseEnum.Mago},
            new Personagem() {Id = 7, Nome = "Ragadast", PontosVida = 100, Forca = 20 , Inteligencia = 20, Defesa = 10, Classe=ClasseEnum.Cavaleiro}
        }; 

//ATIVIDADE 1
    
    [HttpGet("GetByClasse/{classeId}")] //A
        public IActionResult GetByClasse (int classeId)
        {
            List<Personagem> pEncontrado = personagens.FindAll(p=> (int)p.Classe == classeId);
            return Ok(pEncontrado);
        }
        
        
        [HttpGet("GetByNome/{nome}")] //B
        public IActionResult GetByNome(string nome) 
        {
            List<Personagem> listaBusca = personagens.FindAll(P => P.Nome == nome);

            if(listaBusca.Count == 0){
            return BadRequest("Not Found");
            }
            else{
            return Ok(listaBusca);
            }
        }

        [HttpPost("PostValidacao")] //C
        public IActionResult PostValidacao(Personagem NovoPersonagem)
        {
            personagens.Add(NovoPersonagem);

            if(NovoPersonagem.Inteligencia > 30 || NovoPersonagem.Defesa < 10){
            return BadRequest("O valor da inteligencia nao pode ser menor que 30 ou valor de defesa nao pode ser menor que 10");
            }
            else{
            return Ok(personagens); 
            }
        }

        
        [HttpPost("PostValidacaoMago")] //D
        public IActionResult PostValidacaoMago(Personagem NovoPersonagem)
        {
            personagens.Add(NovoPersonagem);
            if(NovoPersonagem.Inteligencia < 35 && NovoPersonagem.Classe == ClasseEnum.Mago){
                return BadRequest("o personagem nao pode ser mago com inteligencia menor que 35");
            } 
            else{
                return Ok(personagens);
            }
        }

        [HttpGet("GetClerigoMago")] //E
        public IActionResult GetClerigoMago()
        {
            List<Personagem> lista = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro).OrderByDescending(p => p.PontosVida).ToList(); 
            return Ok(lista);
        }

        [HttpGet("GetEstatisticas")] //F
        public IActionResult GetEstatisticas()
        {
            return Ok("Quantidade - Personagens: " + personagens.Count + "\n" + 
            "A soma total da inteligência: " + personagens.Sum(p => p.Inteligencia));

        }
    }
}