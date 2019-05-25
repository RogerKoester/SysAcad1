using SysAcad.DAL;
using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysAcad.Controllers
{
    public class TreinoController : Controller
    {
        // GET: Treino
        public ActionResult Index()
        {
            ViewBag.Usuarios = new SelectList(UsuarioDAO.RetornarUsuarios(), "UsuarioId", "Nome");
     
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }
    
            return View();
        }

        public ActionResult ListaTreino()
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }

            Usuario u = (Usuario) Session["USUARIO"];
            
            return View(TreinoDAO.RetornarTreinoPorUsuario(u));
        }
    }
}