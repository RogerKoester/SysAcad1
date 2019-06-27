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
                TentativasDeTreino = new List<TentativaDeTreino>(),
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


        public ActionResult TreinoAtual()
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
                TreinoAtual t = TreinoAtualDAO.BuscarTreinoAtualUsuario(user);
                if (t != null)
                {
                    ViewBag.TreinoAtual = TreinoDAO.BuscarTreino(t.Treino.TreinoId);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }

            return View();
        }

        public ActionResult IniciarTreino(int? id)
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
                TreinoAtual t = new TreinoAtual();
                Treino treino = TreinoDAO.BuscarTreino(id);
                t.Treino = treino;
                t.Usuario = treino.Usuario;
                Usuario u = UsuarioDAO.BuscarUsuario(user.UsuarioId);
                TentativaDeTreino tentativa = new TentativaDeTreino();
                tentativa.Inicio = DateTime.Now;
                tentativa.Termino = DateTime.Now;
                if (treino.TentativasDeTreino == null)
                {
                    treino.TentativasDeTreino = new List<TentativaDeTreino>();
                }
                treino.TentativasDeTreino.Add(tentativa);
                TreinoDAO.Alterar(treino);

                if (!TreinoAtualDAO.Cadastrar(t))
                {
                    return RedirectToAction("Error");
                }
                else
                {
                    return RedirectToAction("TreinoAtual");
                }
            }
            else
            {
                return RedirectToRoute("Login");
            }
        }
    }
}