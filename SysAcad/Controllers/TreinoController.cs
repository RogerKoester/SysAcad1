using SysAcad.DAL;
using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysAcad.Controllers
{
    public class TreinoController : SuperController
    {
        // GET: Treino
        public ActionResult Index()
        {
            ViewBag.Usuarios = new SelectList(UsuarioDAO.RetornarUsuarios(), "UsuarioId", "Nome");
            ViewBag.Treinos = TreinoDAO.RetornarTreinos();
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario) Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }
    
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(int? Usuarios, string txtDataDeExpiracao ,string txtQuantidade, string txtNome)
        {
            Treino t = new Treino
            {
                Nome = txtNome,
                Usuario = UsuarioDAO.BuscarUsuario(Usuarios),
                DataExpiracao = Convert.ToDateTime(txtDataDeExpiracao),
                QuantidadeTreinos = Convert.ToInt16(txtQuantidade),
                TreinosRealizados = new List<Finalizacao>(),
                ItensTreino = new List<ItemTreino>()
            };
            if (TreinoDAO.CadastrarTreino(t))
            {
                return RedirectToAction("Sucess");
            }

            return RedirectToAction("Error");

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