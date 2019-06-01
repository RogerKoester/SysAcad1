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
        List<Exercicio> exercicios = new List<Exercicio>();
        // GET: Treino
        public ActionResult Index()
        {
            ViewBag.Usuarios = new SelectList(UsuarioDAO.RetornarUsuarios(), "UsuarioId", "Nome");
            ViewBag.Treinos = TreinoDAO.RetornarTreinos();
            exercicios = ExercicioDAO.RetornarExercicios();
            ViewBag.Exercicios = exercicios;
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario) Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }
    
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(int? Usuarios, string txtDataDeExpiracao ,string txtQuantidade, string txtNome, bool[] listExercicios, string[] repeticoes, string[] series)
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