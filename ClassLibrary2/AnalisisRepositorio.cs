using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class AnalisisRepositorio : RepositorioBase<Analisis>
    {
        public override Analisis Buscar(int id)
        {
            Analisis analisis = new Analisis();
            try
            {
                analisis = _contexto.analisis.Find(id);
                analisis.detalle.Count();
                foreach (var item in analisis.detalle)
                {
                    string s = item.Tipo.Descripcion;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return analisis;
        }

        public override bool Modificar(Analisis analisis)
        {
            bool paso = false;
            try
            {
                var Anterior = _contexto.analisis.Find(analisis.AnalisisID);
                foreach (var item in Anterior.detalle)
                {
                    if (!analisis.detalle.Exists(d => d.ID == item.ID))
                    {
                        item.Tipo = null;
                        _contexto.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var item in analisis.detalle)
                {

                    var estado = item.ID > 0 ? EntityState.Modified : EntityState.Added;
                    if (_contexto.SaveChanges() > 0)
                    {
                        paso = true;
                    }
                    _contexto.Entry(analisis).State = EntityState.Modified;
                    if (_contexto.SaveChanges() > 0)
                        paso = true;



                }
            }
            catch
            {
                throw;
            }
            return paso;
        }

    }
}
