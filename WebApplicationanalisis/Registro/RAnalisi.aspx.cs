using BLL;
using ClassLibrary1;
using ClassLibrary2;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilitarios;

namespace WebApplicationanalisis.Registro
{
    public partial class RAnalisi : System.Web.UI.Page
    {
        private Analisis analisis = new Analisis();
        private RepositorioBase<Analisis> repositorio = new RepositorioBase<Analisis>();
        private RepositorioBase<TipoAnalisis> repositorioBase = new RepositorioBase<TipoAnalisis>();
        private AnalisisRepositorio repositoriod = new AnalisisRepositorio();
        private List<AnalisisDetalle> detalles = new List<AnalisisDetalle>();
        int a = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EnableViewState = true;
                ViewState.Add("Detalle", detalles);
                ViewState.Add("Analisis", analisis);
                ViewState.Add("Index", a);

              
            }
            else
            {
                detalles = (List<AnalisisDetalle>)ViewState["Detalle"];
            }

        }

        protected void BindGrid()
        {
            DetalleGridView.DataSource = ((Analisis)ViewState["Analisis"]).detalle;
            DetalleGridView.DataBind();
        }
        private List<AnalisisDetalle> ListaVacia()
        {
            List<AnalisisDetalle> analisisDetalles = new List<AnalisisDetalle>();
            analisisDetalles.Add(new AnalisisDetalle());
            return analisisDetalles;
        }

        private Analisis LlenaClase()
        {
            // Analisis analisis = new Analisis();
            analisis = (Analisis)ViewState["Analisis"];

            analisis.AnalisisID = Utils.ToInt(idTextBox.Text);
            analisis.Descripcion = descripcionTextBox.Text;
            analisis.detalle = detalles;
            return analisis;

        }

        private void Limpiar()
        {
            idTextBox.Text = string.Empty;
            descripcionTextBox.Text = string.Empty;
            tipoDropDownList.SelectedIndex = 0;
            DetalleGridView.DataSource = null;
            DetalleGridView.DataBind();
        }

        private void LlenarCampo(Analisis analisis)
        {
            tipoDropDownList.DataSource = repositorio.GetList(x => true);
            tipoDropDownList.DataValueField = "ID";
            tipoDropDownList.DataTextField = "Descripcion";
            tipoDropDownList.AppendDataBoundItems = true;
            tipoDropDownList.DataBind();
            DetalleGridView.DataSource = analisis.detalle;
            DetalleGridView.DataBind();
        }

        private void llenarDrownList()
        {
            tipoDropDownList.DataSource = repositorio.GetList(x => true);
            tipoDropDownList.DataValueField = "ID";
            tipoDropDownList.DataTextField = "Descripcion";
            tipoDropDownList.AppendDataBoundItems = true;
            tipoDropDownList.DataBind();


        }

        protected void buscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Analisis> rep = new RepositorioBase<Analisis>();
            Analisis a = rep.Buscar(Utils.ToInt(idTextBox.Text));
            if (a != null)
            {
                LlenarCampo(a);
                ViewState["detalle"] = a.detalle;
            }
            else
            {
                Utils.ShowToastr(this.Page, "No Exite intete otra vez", "Error", "error");
                Limpiar();
            }

        }

        protected void AddLinkButton_Click(object sender, EventArgs e)
        {
            Analisis analisis = new Analisis();
            analisis = (Analisis)ViewState["Analisis"];

            analisis.detalle.Add(new AnalisisDetalle(tipoDropDownList.SelectedIndex = 0));
            ViewState["detalle"] = analisis.detalle;
            this.BindGrid();
            DetalleGridView.Columns[1].Visible = false;

        }



        protected void RemoveLinkButton_Click(object sender, EventArgs e)
        {
            if (DetalleGridView.Rows.Count > 0 && DetalleGridView.SelectedIndex >= 0)
            {
                int indice = int.Parse(ViewState["Index"].ToString());
                detalles.RemoveAt(indice);
                ViewState["detalle"] = detalles;
                DetalleGridView.DataSource = detalles;
                DetalleGridView.DataBind();

                RepositorioBase<Analisis> re = new RepositorioBase<Analisis>();
            }

        }

        protected void LimpiarButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            AnalisisRepositorio repositorio = new AnalisisRepositorio();
            Analisis analisis = repositorio.Buscar(Utils.ToInt(idTextBox.Text));


            if (analisis == null)
            {
                if (repositorio.Guardar(LlenaClase()))
                {

                    Utils.ShowToastr(this.Page, "Guardado con exito!!", "Guardado", "success");
                    Limpiar();
                }
                else
                {
                    Utils.ShowToastr(this.Page, "Revisar todos los campo", "Error", "error");
                    Limpiar();
                }

            }
            else
            {
                if (repositorio.Modificar(LlenaClase()))
                {
                    Utils.ShowToastr(this.Page, "Modificado con exito!!", "Guardado", "success");
                    Limpiar();
                }
                else
                {
                    Utils.ShowToastr(this.Page, "Revisar todos los campo", "Error", "error");
                    Limpiar();
                }
            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            GridViewRow grid = DetalleGridView.SelectedRow;
            List<AnalisisDetalle> lista = (List<AnalisisDetalle>)ViewState["Detalle"];
            RepositorioBase<Analisis> repositorio = new RepositorioBase<Analisis>();
            Analisis analisis = repositorio.Buscar(Utils.ToInt(idTextBox.Text));

            if (IsValid)
            {
                if (analisis != null)
                {
                    repositorio.Eliminar(analisis.AnalisisID);
                    DetalleGridView.DataSource = ViewState["Detalle"];
                    DetalleGridView.DataBind();

                    Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Eliminado", "success");
                    Limpiar();
                }
                else
                {
                    Utils.ShowToastr(this.Page, "Revisar todos los campo", "Error", "error");
                    Limpiar();
                }
            }
        }
    }
}
