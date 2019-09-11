using BLL;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;

namespace WebApplicationanalisis.Registro
{
    public partial class RtiposAnalisis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Limpiar()
        {
            IdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected TipoAnalisis LlenaClase(TipoAnalisis tipoAnalisis)
        {
            tipoAnalisis.TipoAnalisisID = Utils.ToInt(IdTextBox.Text);
            tipoAnalisis.Descripcion = DescripcionTextBox.Text;
            return tipoAnalisis;
        }

        private void LlenaCampos(TipoAnalisis tipoAnalisis)
        {
            IdTextBox.Text = Convert.ToString(tipoAnalisis.TipoAnalisisID);
            DescripcionTextBox.Text = tipoAnalisis.Descripcion;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
            TipoAnalisis tipoAnalisis = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));
            return (tipoAnalisis != null);
        }

        protected void GuardarButton_Click1(object sender, EventArgs e)
        {
            RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
            TipoAnalisis tipoAnalisis = new TipoAnalisis();
            bool paso = false;

            if (IsValid == false)
            {
                Utils.ShowToastr(this.Page, "Revisar todos los campo", "Error", "error");
                return;
            }

            tipoAnalisis = LlenaClase(tipoAnalisis);


            if (tipoAnalisis.TipoAnalisisID == 0)
            {

                if (Utils.ToInt(IdTextBox.Text) > 0)
                {
                    Utils.ShowToastr(this.Page, "Id debe estar en 0", "Revisar", "error");
                    return
                        ;
                }
                else
                {
                    paso = repositorio.Guardar(tipoAnalisis);
                    Utils.ShowToastr(this.Page, "Guardado con exito!!", "Guardado", "success");
                    Limpiar();
                }
            }
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = repositorio.Modificar(tipoAnalisis);
                    Utils.ShowToastr(this.Page, "Modificado con exito!!", "Modificado", "success");
                    Limpiar();
                }
                else
                    Utils.ShowToastr(this.Page, "Este usuario no existe", "Error", "error");
            }
        }

        protected void EliminarButton_Click1(object sender, EventArgs e)
        {
            if (Utils.ToInt(IdTextBox.Text) > 0)
            {
                int id = Convert.ToInt32(IdTextBox.Text);
                RepositorioBase<TipoAnalisis> repositorio = new RepositorioBase<TipoAnalisis>();
                if (repositorio.Eliminar(id))
                {

                    Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Eliminado", "info");
                }
                else
                    Utils.ShowToastr(this.Page, "Fallo al Eliminar :(", "Error", "error");
                Limpiar();
            }
            else
            {
                Utils.ShowToastr(this.Page, "No se pudo eliminar, usuario no existe", "error", "error");
            }
        }
    }
    }
