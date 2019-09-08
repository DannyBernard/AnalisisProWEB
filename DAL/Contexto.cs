using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ClassLibrary1;

namespace DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Analisis> analisis { get; set; }
        public DbSet<TipoAnalisis> tipoAnalisis { get; set; }

        public Contexto(): base("ConStr")
        {

        }


    }
}
