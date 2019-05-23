using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SysAcad.DAL
{
    public class ExercicioDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static Exercicio BuscarExercicioPorNome(Exercicio e)
        {
            return ctx.Exercicios.FirstOrDefault
                (x => x.Nome.Equals(e.Nome));
        }

        public static List<Exercicio> RetornarExercicios()
        {
            return ctx.Exercicios.ToList();
        }
        public static bool ExcluirExercicio(int? id)
        {
            
            Exercicio exercicio = ctx.Exercicios.Find (id);
            ctx.Exercicios.Remove(exercicio);
            ctx.SaveChanges();

            return true;
        }


        public static bool CadastrarExercicio(Exercicio e)
        {
            Exercicio exercicio = BuscarExercicioPorNome(e);
            if (exercicio == null)
            {
                ctx.Exercicios.Add(e);
                ctx.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool AlterarExercicio(Exercicio e)
        {
            Exercicio exercicio = ctx.Exercicios.Find(e.ExercicioId);
            DbEntityEntry<Exercicio> ee = ctx.Entry(exercicio);
            ee.CurrentValues.SetValues(e);
            ctx.SaveChanges();
            return true;
        }
    }
}