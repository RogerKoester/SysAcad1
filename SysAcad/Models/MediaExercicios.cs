using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    public class MediaExercicios
    {
        public Exercicio Exercicio { get; set; }
        public Usuario UsuarioQueMaisFez { get; set; }
        public double MediaDePeso { get; set; }
        public double MediaDeTempo { get; set; }
        public List<Peso> Pesos { get; set; }
        public List<Tempo> Tempos { get; set; }
    }
}