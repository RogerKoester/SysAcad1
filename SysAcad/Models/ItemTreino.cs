using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("ItensTreino")]
    public class ItemTreino
    {
        [Key]
        public int ItemTreinoId { get; set; }
        public Exercicio Exercicio { get; set; }
        public int Repeticoes { get; set; }
        public int Series { get; set; }
    }
}