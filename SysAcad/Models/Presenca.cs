using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Presencas")]
    public class Presenca
    {
        [Key]
        public int PresencaId { get; set; }
        public DateTime Chegada { get; set; }
        
        public DateTime Saida { get; set; }
    }
}