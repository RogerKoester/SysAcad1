using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public int Idade { get; set; }
        public string Genero { get; set; }
        public List<Presenca> Presencas { get; set; }
        public int Peso { get; set; }
        public double Altura { get; set; }
        public Periodo PeriodoPreferido { get; set; }
        public string Senha { get; set; }
        public bool IsAdmin { get; set; }
    }
}