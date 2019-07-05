using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAcad.Models;

namespace SysAcad.DAL
{
    public class TempoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static void Cadastrar(Tempo tempo)
        {
            ctx.Tempos.Add(tempo);
            ctx.SaveChanges();
        }

        public static List<Tempo> RetornarTempoPorExercicio(Exercicio exercicio)
        {
            return ctx.Tempos.Where(tempo => tempo.Exercicio.ExercicioId == exercicio.ExercicioId).ToList();
        }
    }
}