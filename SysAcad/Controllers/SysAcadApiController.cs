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
        public List<Exercicio> GetExercicios()
        {
            return ExercicioDAO.RetornarExercicios();
        }

        [Route("Treino")]
        public List<Treino> GetTreinos()
        {
            return TreinoDAO.RetornarTreinos();
        }
        [Route("TreinoPorUsuario/{id}")]
        public dynamic GetTreinosPorUsuario(int id)
        {
            Usuario u = UsuarioDAO.BuscarUsuario(id);

            List<Treino> GetTreino = TreinoDAO.RetornarTreinoPorUsuario(u);
            if (u == null)
            {
                return NotFound();
            }
            return GetTreino;
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

        [Route("MediaExercicios")]
        public dynamic GetMediaExercicios()
        {
            List<MediaExercicios> medias = new List<MediaExercicios>();
            List<Exercicio> exercicios = ExercicioDAO.RetornarExercicios();

            foreach (Exercicio e in exercicios)
            {
                List<Tempo> tempos = TempoDAO.RetornarTempoPorExercicio(e);
                List<Peso> pesos = PesoDAO.RetornarPesoPorExercicio(e);
                MediaExercicios media = new MediaExercicios();
                Usuario u = new Usuario();
                media.Pesos = pesos;
                media.Tempos = tempos;
                var todosOsTempos = new List<double>();
                var todosOsPesos = new List<double>();
                foreach (Tempo tempo in tempos)
                {
                    todosOsTempos.Add(tempo.TempoDecorrido);
                }
                foreach (Peso peso in pesos)
                {
                    todosOsPesos.Add(peso.Quantidade);
                }
                
                
                if (todosOsPesos.Count > 0)
                {
                    var arrayPesos = todosOsPesos.ToArray();
                    double avgPeso = Queryable.Average(arrayPesos.AsQueryable());
                    media.MediaDePeso = avgPeso;

                }
                else
                {
                    media.MediaDePeso = 0;
                }
                if (todosOsTempos.Count > 0)
                {
                    var arrayTempo = todosOsTempos.ToArray();
                    double avgTempo = Queryable.Average(arrayTempo.AsQueryable());
                    media.MediaDeTempo = avgTempo;
                }
                else
                {
                    media.MediaDeTempo = 0;
                }


                if (tempos.Count > 0)
                {
                    u = tempos.GroupBy(a => a.Aluno)
                    .OrderByDescending(g => g.Count())
                    .First().Key;
                }
                else if (pesos.Count > 0)
                {
                    u = pesos.GroupBy(a => a.Aluno)
                    .OrderByDescending(g => g.Count())
                    .First().Key;
                }

                
                
                media.UsuarioQueMaisFez = u;
                media.Exercicio = e;
                medias.Add(media);

            }

            return medias;
        }
    }
}
