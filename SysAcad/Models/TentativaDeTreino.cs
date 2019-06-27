using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("TentativasDeTreino")]
    public class TentativaDeTreino
    {
        [Key]
        public int TentativaDeTreinoId { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Termino { get; set; }
        public Exercicio ExercicioAtual { get; set; }

    }
}