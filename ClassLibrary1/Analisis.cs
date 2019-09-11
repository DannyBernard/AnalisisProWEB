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
       

        public Analisis(int analisisID,string descripcion) { 
            AnalisisID = analisisID;
            Descripcion = descripcion;
         
        }

        public List<AnalisisDetalle> detalle { get; set; }

        public Analisis()
        {
            this.detalle = new List<AnalisisDetalle>();
        }

        public void AgregarDetalle(int id, int analisisID, string descripcion)
        {

        }

    }
}
