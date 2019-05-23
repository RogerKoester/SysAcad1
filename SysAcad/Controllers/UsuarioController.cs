using SysAcad.DAL;
using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysAcad.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }
            ViewBag.Usuarios = UsuarioDAO.RetornarUsuarios();
            return View();
        }

        public ActionResult Alterar(string nome)
        {
            if (Session["USUARIO"] != null)
            {
                Usuario user = (Usuario)Session["USUARIO"];
                ViewBag.IsAdmin = user.IsAdmin;
                ViewBag.Usuario = user;
            }
            Usuario usuario = new Usuario();
            usuario.Nome = nome;
            ViewBag.Usuario = UsuarioDAO.BuscarUsuarioPorNome(usuario);

            return View();
        }

        [HttpPost]
        public ActionResult Alterar(int hdnId, string txtNome, string txtCpf, string txtIdade, string txtGenero, string txtPeso, string txtAltura, string txtSenha)
        {
            Usuario us = UsuarioDAO.BuscarUsuario(hdnId);
            us.Nome = txtNome;
            us.CPF = txtCpf;
            us.Idade = Convert.ToInt32(txtIdade);
            us.Genero = txtGenero;
            us.Peso = Convert.ToInt32(txtPeso);
            us.Altura = Convert.ToDouble(txtAltura);
            us.Senha = txtSenha;

            if (UsuarioDAO.Alterar(us))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult TornarAdmin(int? id)
        {
            if (id != null)
            {
                UsuarioDAO.TornarAdmin(id);
            }
            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Remover(int? id)
        {
            if (id == null)
            {

            }
            else
            {
                UsuarioDAO.ExcluirUsuario(id);
            }

            return RedirectToAction("Index", "Usuario");
        }

    }
}