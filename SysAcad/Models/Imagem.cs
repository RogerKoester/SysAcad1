using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Imagens")]
    public class Imagem
    {

        public string Caminho { get; set; }
        [Key]
        public int ImagemId { get; set; }

    }
}