using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Exercicios")]
    public class Exercicio
    {
        [Key]
        public int ExercicioId { get; set; }
        public string Nome { get; set; }
        public string Link { get; set; }
        public Usuario UsuarioDono { get; set; }
    }
}