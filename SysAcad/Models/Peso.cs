using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Pesos")]
    public class Peso
    {
        [Key]
        public int PesoId { get; set; }
        public double Quantidade { get; set; }
        public Exercicio Exercicio { get; set; }
        public Usuario Aluno { get; set; }
        public DateTime Data { get; set; }
    }
}