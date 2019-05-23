using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Finalizacoes")]
    public class Finalizacao
    {
        [Key]
        public int FinalizacaoId { get; set; }
        public DateTime Data { get; set; }
        public int QuantidadeRealizada { get; set; }
    }
}