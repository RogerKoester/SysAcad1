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

        public static Treino BuscarTreinoAtualUsuario(Usuario u )
        {
            return ctx.Treinos.FirstOrDefault(x => x.Usuario.UsuarioId == u.UsuarioId);
        }

        public static void Cadastrar(TreinoAtual t)
        {
            ctx.TreinosAtuais.Add(t);
            ctx.SaveChanges();
        }
    }
}