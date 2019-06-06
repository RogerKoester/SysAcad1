using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysAcad.DAL
{
    public class TreinoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static Treino BuscarTreino(Treino e)
        {
            return ctx.Treinos.Find(e.TreinoId);
        }

        public static List<Treino> RetornarTreinoPorUsuario(Usuario u)
        {
            return ctx.Treinos.Include("ItensTreino.Exercicio").Include("TreinosRealizados").Where(x => x.Usuario.UsuarioId.Equals(u.UsuarioId)).ToList();
        }

        public static List<Treino> RetornarTreinos()
        {
            return ctx.Treinos.Include("ItensTreino.Exercicio").Include("TreinosRealizados").ToList();
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
    }
}