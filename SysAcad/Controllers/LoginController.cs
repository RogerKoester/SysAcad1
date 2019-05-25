using SysAcad.DAL;
using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysAcad.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["USUARIO"] = null;
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Login(string txtLogin, string txtSenha)
        {
            Usuario u = new Usuario();
            u.CPF = txtLogin;
            u.Senha = txtSenha;
            Usuario user = UsuarioDAO.BuscarUsuarioPorCPF(u);
            if(user != null)
            {
                if (user.Senha.Equals(u.Senha))
                {
                    Session["USUARIO"] = user;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Error", "Login"); ;
        }

        [HttpPost]
        public ActionResult Cadastrar(string Nome, string CPF, int Idade, string Genero, int Peso, double Altura, string Senha)
        {
            Usuario u = new Usuario
            {
                Nome = Nome,
                CPF = CPF,
                Idade = Idade,
                Genero = Genero,
                Peso = Peso,
                Altura = Altura,
                Senha = Senha,
                IsAdmin = false
            };

            Periodo periodoPreferido = Periodo.calcularHorarioPreferido(u);
            if (UsuarioDAO.CadastrarUsuario(u))
            {
                Session["USUARIO"] = u;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Login");
            }
            
        }

        public ActionResult Cadastro()
        {
            return View(); 
        }
    }
}