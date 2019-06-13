using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("TreinosAtuais")]
    public class TreinoAtual
    {
        [Key]
        public int TreinoAtualId { get; set; }
        public Treino Treino { get; set; }
        public Usuario Usuario { get; set; }

    }
}