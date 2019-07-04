using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysAcad.DAL
{
    public class TreinoAtualDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static TreinoAtual BuscarTreinoAtualUsuario(Usuario u)
        {
            return ctx.TreinosAtuais.Include("Usuario").Include("Treino.TentativasDeTreino.ItemTreinoAtual").FirstOrDefault(x => x.Usuario.UsuarioId == u.UsuarioId);
        }

        public static bool Cadastrar(TreinoAtual t)
        {
            TreinoAtual treino = BuscarTreinoAtualUsuario(t.Usuario);
            if (treino == null)
            {
                ctx.TreinosAtuais.Add(t);
                ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool Excluir(TreinoAtual t)
        {
            ctx.TreinosAtuais.Remove(t);
            ctx.SaveChanges();
            return true;
        }
    }
}