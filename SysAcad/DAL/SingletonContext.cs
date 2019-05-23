using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysAcad.DAL
{
    class SingletonContext
    {
        private SingletonContext() { }

        private static Context context;

        public static Context GetInstance()
        {
            if (context == null)
            {
                context = new Context();
            }
            return context;
        }
    }
}