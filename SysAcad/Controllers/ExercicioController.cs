using Firebase.Storage;
using SysAcad.DAL;
using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysAcad.Controllers
{
    public class ExercicioController : SuperController
    {
        // GET: Exercicio
        public ActionResult Index()
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }
            ViewBag.ListaExercicios = ExercicioDAO.RetornarExercicios();
            return View();
        }
        public ActionResult Remover(int? id)
        {
            if (id == null)
            {

            }
            else
            {
                ExercicioDAO.ExcluirExercicio(id);
            }

            return RedirectToAction("Index", "Exercicio");
        }
        public ActionResult Alterar(string nome)
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }
            Exercicio exercicio = new Exercicio();
            exercicio.Nome = nome;
            ViewBag.Exercicio = ExercicioDAO.BuscarExercicioPorNome(exercicio);
            return View();
        }
        [HttpPost]
        public ActionResult Alterar(string txtNome, string txtLink, int hdnId, int hdnDonoId)
        {
            Exercicio e = new Exercicio
            {
                Nome = txtNome,
                Link = txtLink,
                ExercicioId = hdnId
            };
            e.UsuarioDono = UsuarioDAO.BuscarUsuario(hdnDonoId);
            if (ExercicioDAO.AlterarExercicio(e))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Cadastrar(string Nome, string Link, HttpPostedFileBase fupImagem)
        {
            Exercicio e = new Exercicio
            {
                Nome = Nome,
                Link = Link,
                Imagens = new List<Imagem>()
            };
            if (fupImagem != null)
            {
                //TODO criar logica de nome para as imagens
                //TODO descobrir como mostrar a imagem a partir do link
                var task = new FirebaseStorage("sysacad-23935.appspot.com")
                .Child("main")
                .Child((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString())
                .PutAsync(fupImagem.InputStream);
                var downloadUrl = await task;
                Imagem imagem = new Imagem();
                imagem.Caminho = downloadUrl;
                e.Imagens.Add(imagem);
            }
            if (Session["USUARIO"] != null)
            {
                e.UsuarioDono = (Usuario)Session["USUARIO"];
            }
            else
            {
                return RedirectToAction("Error");
            }
            if (ExercicioDAO.CadastrarExercicio(e))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

    }
}