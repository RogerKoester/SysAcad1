using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SysAcad.DAL
{
    public class TreinoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static Treino BuscarTreino(int? e)
        {
            return ctx.Treinos.Include("Usuario").Include("ItensTreino.Exercicio.Imagens").Include("TentativasDeTreino.ItemTreinoAtual").FirstOrDefault(x => x.TreinoId == e);
        }

        public static List<Treino> RetornarTreinoPorUsuario(Usuario u)
        {
            return ctx.Treinos.Include("ItensTreino.Exercicio.Imagens").Include("TentativasDeTreino.ItemTreinoAtual").Where(x => x.Usuario.UsuarioId.Equals(u.UsuarioId)).ToList();
        }

        public static List<Treino> RetornarTreinos()
        {
            return ctx.Treinos.Include("ItensTreino.Exercicio").Include("TentativasDeTreino.ItemTreinoAtual").ToList();
        }

        public static Treino BuscarTreinoPorNome(Treino t)
        {
            return ctx.Treinos.Include("ItensTreino.Exercicio").Include("TreinosRealizados").FirstOrDefault(x => x.Nome.Equals(t.Nome));
        }

        public static bool CadastrarTreino(Treino treino)
        {
            ctx.Treinos.Add(treino);
            ctx.SaveChanges();
            return true;
        }

        public static bool Alterar(Treino t)
        {
            Treino treino = ctx.Treinos.Find(t.TreinoId);
            DbEntityEntry<Treino> ee = ctx.Entry(treino);
            ee.CurrentValues.SetValues(treino);
            ctx.SaveChanges();
            return true;
        }
    }
}