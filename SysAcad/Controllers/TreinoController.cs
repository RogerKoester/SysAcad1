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
            List<Exercicio> exercicios = new List<Exercicio>();
            ViewBag.Usuarios = new SelectList(UsuarioDAO.RetornarUsuarios(), "UsuarioId", "Nome");
            ViewBag.Treinos = TreinoDAO.RetornarTreinos();
            exercicios = ExercicioDAO.RetornarExercicios();
            ViewBag.Exercicios = exercicios;
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }

            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(int? Usuarios, string txtDataDeExpiracao, string txtQuantidade, string txtNome, int[] checkbox, string[] repeticoes, string[] series)
        {
            List<Exercicio> exercicios = new List<Exercicio>();
            foreach (int i in checkbox)
            {
                Exercicio e = ExercicioDAO.BuscarExercicio(i);
                if (e != null)
                {
                    exercicios.Add(e);
                }
            }
            List<String> repeticoesDigitadas = new List<string>();
            List<String> seriesDigitadas = new List<string>();

            foreach (string s in repeticoes)
            {
                if (!s.Equals(""))
                {
                    repeticoesDigitadas.Add(s);
                }
            }

            foreach (string s in series)
            {
                if (!s.Equals(""))
                {
                    seriesDigitadas.Add(s);
                }
            }

            List<ItemTreino> itensTreino = new List<ItemTreino>();

            for (int i = 0; i < exercicios.Count; i++)
            {
                ItemTreino item = new ItemTreino
                {
                    Exercicio = exercicios[i],
                    Repeticoes = Convert.ToInt16(repeticoesDigitadas[i]),
                    Series = Convert.ToInt16(seriesDigitadas[i])
                };
                itensTreino.Add(item);
            }

            Treino t = new Treino
            {
                Nome = txtNome,
                Usuario = UsuarioDAO.BuscarUsuario(Usuarios),
                DataExpiracao = Convert.ToDateTime(txtDataDeExpiracao),
                QuantidadeTreinos = Convert.ToInt16(txtQuantidade),
                TreinosRealizados = new List<Finalizacao>(),
                ItensTreino = itensTreino
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

            Usuario u = (Usuario)Session["USUARIO"];

            return View(TreinoDAO.RetornarTreinoPorUsuario(u));
        }
    }
}