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
    }
}