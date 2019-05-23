using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysAcad.DAL
{
    public class PeriodoDAO
    {
        private static Context ctx = SingletonContext.GetInstance();

        public static List<Periodo> RetornarListaDePeriodos()
        {
            return ctx.Periodos.ToList();
        }
    }
}