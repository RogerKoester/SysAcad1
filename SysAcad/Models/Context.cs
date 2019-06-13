using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    public class Context : DbContext
    {
        public Context() : base("DbSysAcad") { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<ItemTreino> ItensTreino { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
        public DbSet<Peso> Pesos { get; set; }
        public DbSet<Presenca> Presencas { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<Finalizacao> Finalizacoes { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<TreinoAtual> TreinosAtuais { get; set; }

    }
}