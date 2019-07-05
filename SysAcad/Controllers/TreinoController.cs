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
        public ActionResult Cadastrar(int? Usuarios, string txtDataDeExpiracao, string txtQuantidade, string txtNome, int[] checkbox, string[] repeticoes, string[] series, string[] horas, string[] minutos)
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

            List<ItemTreino> itensTreino = new List<ItemTreino>();

            for (int i = 0; i < exercicios.Count; i++)
            {
                ItemTreino item = new ItemTreino();
                item.Exercicio = exercicios[i];
                if (!repeticoes[i].Equals(""))
                {
                    item.Repeticoes = Convert.ToInt16(repeticoes[i]);
                }
                if (!minutos[i].Equals(""))
                {
                    item.Minutos = Convert.ToDouble(minutos[i]);
                }
                if (!horas[i].Equals(""))
                {
                    item.Horas = Convert.ToDouble(horas[i]);
                }
                if (!series[i].Equals(""))
                {
                    item.Series = Convert.ToInt16(series[i]);
                }

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

        public ActionResult Proximo(string txtMedida,int hdnId,string unidades)
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
                TreinoAtual t = TreinoAtualDAO.BuscarTreinoAtualUsuario(user);
                Treino treino = t.Treino;
                TentativaDeTreino tentativaAtual = TentativaDeTreinoDAO.BuscarTentativaDeTreino(treino.TentativasDeTreino.Last().TentativaDeTreinoId);
                if (!(hdnId + 1 > treino.ItensTreino.Count))
                {
                    tentativaAtual.ItemTreinoAtual = treino.ItensTreino[hdnId + 1];
                }
                else
                {
                    RedirectToAction("TreinoAtual");
                }
                
                TentativaDeTreinoDAO.Alterar(tentativaAtual);
                if (unidades.Equals("peso"))
                {
                    Peso peso = new Peso();
                    peso.Exercicio = tentativaAtual.ItemTreinoAtual.Exercicio;
                    peso.Data = DateTime.Now;
                    peso.Aluno = user;
                    peso.Quantidade = Convert.ToDouble(txtMedida);
                    PesoDAO.Cadastrar(peso);
                }
                else if (!(txtMedida.Equals("")))
                {
                    Tempo tempo = new Tempo();
                    tempo.Exercicio = tentativaAtual.ItemTreinoAtual.Exercicio;
                    tempo.Data = DateTime.Now;
                    tempo.Aluno = user;
                    tempo.TempoDecorrido = Convert.ToDouble(txtMedida);
                    TempoDAO.Cadastrar(tempo);
                }
                if (t != null)
                {
                    ViewBag.TreinoAtual = TreinoDAO.BuscarTreino(t.Treino.TreinoId);
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }

            return RedirectToAction("TreinoAtual");
        }

        public ActionResult Finalizar()
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
                TreinoAtual t = TreinoAtualDAO.BuscarTreinoAtualUsuario(user);
                Treino treino = TreinoDAO.BuscarTreino(t.Treino.TreinoId);
                treino.TentativasDeTreino.Last().Termino = DateTime.Now;
                TreinoDAO.Alterar(treino);
                TreinoAtualDAO.Excluir(t);
            }
            return RedirectToAction("ListaTreino");
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
                t.Usuario = treino.Usuario;
                Usuario u = UsuarioDAO.BuscarUsuario(user.UsuarioId);
                TentativaDeTreino tentativa = new TentativaDeTreino();
                tentativa.Inicio = DateTime.Now;
                tentativa.Termino = DateTime.Now;
                tentativa.ItemTreinoAtual = treino.ItensTreino.First();
                if (treino.TentativasDeTreino == null)
                {
                    treino.TentativasDeTreino = new List<TentativaDeTreino>();
                }
                treino.TentativasDeTreino.Add(tentativa);
                t.Treino = treino;
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