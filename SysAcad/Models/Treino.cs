using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Treinos")]
    public class Treino
    {
        [Key]
        public int TreinoId { get; set; }
        public int QuantidadeTreinos { get; set; }
        public Usuario Usuario { get; set; }
        public List<ItemTreino> ItensTreino { get; set; }
        public List<Finalizacao> TreinosRealizados { get; set; }
        public DateTime DataExpiracao { get; set; }
        public object Nome { get; internal set; }
    }
}