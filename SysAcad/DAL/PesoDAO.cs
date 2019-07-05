using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysAcad.Models;

namespace SysAcad.DAL
{
    public class PesoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();
        public static void Cadastrar(Peso peso)
        {
            ctx.Pesos.Add(peso);
            ctx.SaveChanges();
        }
        public static List<Peso> RetornarPesoPorExercicio(Exercicio exercicio)
        {
            return ctx.Pesos.Where(peso => peso.Exercicio.ExercicioId == exercicio.ExercicioId).ToList();
        }
    }
}