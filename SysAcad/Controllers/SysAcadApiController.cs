using SysAcad.DAL;
using SysAcad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SysAcad.Controllers
{
    [RoutePrefix("Api")]
    public class SysAcadApiController : ApiController
    {
        [Route("Exercicios")]
        public List<Exercicio> GetExercicios ()
        {
            return ExercicioDAO.RetornarExercicios();
        }

        [Route("Usuario/{id}")]
        public dynamic GetUsuario(int id)
        {
            Usuario u = UsuarioDAO.BuscarUsuario(id);

            if (u == null)
            {
                return NotFound();
            }

            return u;
        }
    }
}
