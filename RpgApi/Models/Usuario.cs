using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

//[Required] //coluna seja notnull
        public string Perfil { get; set; }

[NotMapped] //nao gerar coluna no BD
        public string PasswordString{ get ; set ;}
        
        public byte[] Foto { get; set; }
        public List<Personagem> Personagens { get; set; }
    }
}