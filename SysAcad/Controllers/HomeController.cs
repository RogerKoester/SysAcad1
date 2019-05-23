using SysAcad.DAL;
using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysAcad.Controllers
{
    public class HomeController : SuperController
    {
        public ActionResult Index()
        {
            if(Session["USUARIO"] != null)
            {
                Usuario user = (Usuario) Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }
            return View();
        }
    }
}