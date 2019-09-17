using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities

{
    public class Analisis
    {
        [Key]
        public int AnalisisID { get; set; }
        [StringLength(100)]
        public string  Descripcion { get; set; }
        public string paciente { get; set; }
        public float Balance { get; set; }
        public float Monto { get; set; }


        public Analisis(int analisisID,string descripcion) { 
            AnalisisID = analisisID;
            Descripcion = descripcion;
         
        }

        public List<AnalisisDetalle> detalle { get; set; }

        public Analisis()
        {
            this.detalle = new List<AnalisisDetalle>();
            AnalisisID = 0;
            Descripcion = string.Empty;
            Balance = 0;
            Monto = 0;
        }

       

    }
}
