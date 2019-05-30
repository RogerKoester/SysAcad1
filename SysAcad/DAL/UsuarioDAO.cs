using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SysAcad.DAL
{
    public class UsuarioDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static Usuario BuscarUsuarioPorCPF(Usuario u)
        {
            return ctx.Usuarios.FirstOrDefault
                (x => x.CPF.Equals(u.CPF));
        }
        public static Usuario BuscarUsuarioPorNome(Usuario u)
        {
            return ctx.Usuarios.FirstOrDefault
                (x => x.Nome.Equals(u.Nome));
        }

        public static Usuario BuscarUsuario(int? id)
        {
            return ctx.Usuarios.Find(id);
        }

        public static List<Usuario> RetornarUsuarios()
        {
            return ctx.Usuarios.ToList();
        }

        public static bool CadastrarUsuario(Usuario u)
        {
            Usuario usuario = BuscarUsuarioPorCPF(u);
            if (usuario == null)
            {
                ctx.Usuarios.Add(u);
                ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TornarAdmin(int? id)
        {
            Usuario usuario = ctx.Usuarios.Find(id);
            usuario.IsAdmin = true;
            DbEntityEntry<Usuario> ee = ctx.Entry(usuario);
            ee.CurrentValues.SetValues(usuario);
            ctx.SaveChanges();
            return true;
        }

        public static void ExcluirUsuario(int? id)
        {
            Usuario u = ctx.Usuarios.Find(id);
            ctx.Usuarios.Remove(u);
            ctx.SaveChanges();
        }

        public static bool Alterar(Usuario us)
        {
            Usuario usu = ctx.Usuarios.Find(us.UsuarioId);
            DbEntityEntry<Usuario> ee = ctx.Entry(us);
            ee.CurrentValues.SetValues(us);
            ctx.SaveChanges();
            return true;
        }
    }
}