using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SysAcad.DAL
{
    public class TentativaDeTreinoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static TentativaDeTreino BuscarTentativaDeTreino(int id)
        {
            return ctx.TentativasDeTreino.Find(id);
        }

        public static bool Alterar(TentativaDeTreino tentativaAtual)
        {
            DbEntityEntry<TentativaDeTreino> ee = ctx.Entry(tentativaAtual);
            ee.CurrentValues.SetValues(tentativaAtual);
            ctx.SaveChanges();
            return true;
        }
    }
}