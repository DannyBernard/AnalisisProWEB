using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    [Serializable]
  public  class TipoAnalisis
    {
        [Key]
        public int TipoAnalisisID { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }
        public int Acomulado { get; set; }

        public TipoAnalisis()
        {
            TipoAnalisisID = 0;
            Descripcion = string.Empty;
            Acomulado = 0;

        }

    }
}
