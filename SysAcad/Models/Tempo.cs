using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Tempos")]
    public class Tempo
    {
        [Key]
        public int TempoId { get; set; }
        public double TempoDecorrido { get; set; }
        public Exercicio Exercicio { get; set; }
        public Usuario Aluno { get; set; }
        public DateTime Data { get; set; }
    }
}