using Microsoft.EntityFrameworkCore;
using RpgApi.Models;

namespace RpgApi.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Personagem> Personagens { get; set;}
        public DbSet<Arma> Armas { get; set;}
        public DbSet<Usuario> Usuarios { get; set;}
        public DbSet<Habilidade> Habilidades { get; set;}
        public DbSet<PersonagemHabilidade> PersonagemHabilidades{ get; set;}
         public DbSet<Disputa> Disputas { get; set;}

        //CRIAR METODO SOBRESCRITO PARA MODIFICAR O METODO JA EXISTENTE

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonagemHabilidade>()
            .HasKey(ph => new {ph.PersonagemId, ph.HabilidadeId });
            //editando a tabela para dizer que ela tem chave composta

            modelBuilder.Entity<Usuario>()
            .Property(u => u.Perfil).HasDefaultValue("Jogador");
            //se ele nao for identificado o perfil, ele sera um jogador
        }
    }
}