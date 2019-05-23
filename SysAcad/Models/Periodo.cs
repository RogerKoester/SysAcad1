using SysAcad.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysAcad.Models
{
    [Table("Periodos")]
    public class Periodo
    {
        [Key]
        public int PeriodoId { get; set; }
        public string Nome { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }

        public static Periodo calcularHorarioPreferido(Usuario u)
        {

            List<Periodo> periodos = PeriodoDAO.RetornarListaDePeriodos();
            List<DateTime> datas = new List<DateTime>();
            if(u.Presencas == null){
                u.Presencas = new List<Presenca>();
            }
            foreach (Presenca presenca in u.Presencas)
            {
                datas.Add(presenca.Chegada);
                datas.Add(presenca.Saida);
            }

            var count = datas.Count;
            double temp = 0D;
            for (int i = 0; i < count; i++)
            {
                temp += datas[i].Ticks / (double)count;
            }
            var horarioPreferido = new DateTime((long)temp);

            foreach (Periodo periodo in periodos)
            {
                if(periodo.HoraInicio.Hour  < horarioPreferido.Hour && horarioPreferido.Hour > periodo.HoraFim.Hour)
                {
                    return periodo;
                }
            }
            return null;
        }
    }
}