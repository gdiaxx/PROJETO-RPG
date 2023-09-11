using System.Collections.Generic;
using RpgMvc.Models.Enuns;

namespace RpgMvc.Models
{
    public class PersonagemViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int PontosVida { get; set; }
        public int Forca { get; set; }
        public int Defesa { get; set; }
        public int Inteligencia { get; set; }
        public ClasseEnum Classe { get; set; }
        public string FotoPersonagem { get; set; }
        public int Disputas { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }                
        public UsuarioViewModel Usuario { get; set; }
        public List<PersonagemHabilidadesViewModel> PersonagemHabilidades { get; set; }
        
        //public Arma Arma { get; set; }
        
        //public List<PersonagemHabilidade> PersonagemHabilidades { get; set; }
    }
}