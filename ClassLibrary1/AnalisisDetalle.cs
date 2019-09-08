using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{    
    [Serializable]
    public class AnalisisDetalle
    {
        [Key]
        public int ID { get; set; }
        public int AnalisisID { get; set; }
        public int TipoAnalisisID { get; set; }

        [ForeignKey("TipoAnalisisID")]

        public virtual TipoAnalisis Tipo { get; set; }


        public AnalisisDetalle()
        {
            this.ID = 0;
            this.AnalisisID = 0;

        }

        public AnalisisDetalle( int tipoAnalisisID )
        {

            TipoAnalisisID = tipoAnalisisID;
           
        }
    }
}
