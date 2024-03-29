﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationanalisis.Registro
{
    public partial class RegistroPago : System.Web.UI.Page
    {
        public partial class RegistroPagos : System.Web.UI.Page
        {
            readonly string KeyViewState = "Pagos";
            protected void Page_Load(object sender, EventArgs e)
            {
                FechaTextBox.Text = DateTime.Now.ToFormatDate();
                if (!Page.IsPostBack)
                {
                    ViewState[KeyViewState] = new Pagos();
                    LlenarCombo();
                }
            }
            public void Limpiar()
            {
                PagosIdTextBox.Text = "0";
                AnalisisDropdownList.SelectedIndex = -1;
                PacienteNombreBox.Text = string.Empty;
                BalanceTextBox.Text = string.Empty;
                ViewState[KeyViewState] = new Pagos();
                LlenarCombo();
                this.BindGrid();
            }
            private void LlenarCombo()
            {
                AnalisisDropdownList.Items.Clear();
                RepositorioAnalisis repositorioAnalisis = new RepositorioAnalisis();
                List<Analisis> lista = repositorioAnalisis.GetList(x => x.Balance > 0);
                repositorioAnalisis.Dispose();
                AnalisisDropdownList.DataSource = lista;
                AnalisisDropdownList.DataValueField = "AnalisisID";
                AnalisisDropdownList.DataTextField = "AnalisisID";
                AnalisisDropdownList.DataBind();
                AnalisisDropdownList_TextChanged(null, null);
            }
            private void BindGrid()
            {
                Pagos Pagos = ViewState[KeyViewState].ToPago();
                DetalleGridView.DataSource = Pagos.DetallesPagos;
                DetalleGridView.DataBind();
            }
            protected void NuevoButton_Click(object sender, EventArgs e)
            {
                Limpiar();
            }
            protected void GuadarButton_Click(object sender, EventArgs e)
            {
                if (!Validar())
                    return;
                bool paso = false;
                RepositorioPago repositorio = new RepositorioPago();
                Pagos Pagos = LlenaClase();
                TipoTitulo tipoTitulo = TipoTitulo.OperacionFallida;
                TiposMensajes tiposMensajes = TiposMensajes.RegistroNoGuardado;
                IconType iconType = IconType.error;

                if (Pagos.PagosID == 0)
                    paso = repositorio.Guardar(Pagos);
                else
                {
                    if (ExisteEnLaBaseDeDatos())
                    {
                        Extensores.Extensores.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
                        return;
                    }
                    paso = repositorio.Modificar(Pagos);
                }
                if (paso)
                {
                    Limpiar();
                    tipoTitulo = TipoTitulo.OperacionExitosa;
                    tiposMensajes = TiposMensajes.RegistroGuardado;
                    iconType = IconType.success;
                }
                repositorio.Dispose();
                Extensores.Extensores.Alerta(this, tipoTitulo, tiposMensajes, iconType);
            }
            protected void BuscarButton_Click(object sender, EventArgs e)
            {
                RepositorioPago repositorio = new RepositorioPago();
                Pagos Pagos = repositorio.Buscar(PagosIdTextBox.Text.ToInt());
                if (!Pagos.EsNulo())
                {
                    Limpiar();
                    LlenarCampos(Pagos);
                }
                else
                    Extensores.Extensores.ToastSweet(this, IconType.info, TiposMensajes.RegistroNoEncontrado);
            }
            protected void EliminarButton_Click(object sender, EventArgs e)
            {
                RepositorioPago repositorio = new RepositorioPago();
                int id = PagosIdTextBox.Text.ToInt();
                if (ExisteEnLaBaseDeDatos())
                {
                    Extensores.Extensores.Alerta(this, TipoTitulo.OperacionFallida, TiposMensajes.RegistroInexistente, IconType.error);
                    return;
                }
                else
                {
                    if (repositorio.Eliminar(id))
                    {
                        Extensores.Extensores.Alerta(this, TipoTitulo.OperacionExitosa, TiposMensajes.RegistroEliminado, IconType.success);
                        Limpiar();
                    }
                }

            }
            private void LlenarCampos(Pagos pagos)
            {
                PagosIdTextBox.Text = pagos.PagosID.ToString();
                ViewState[KeyViewState] = pagos;
                this.BindGrid();
            }

            private Pagos LlenaClase()
            {
                Pagos Pagos = ViewState[KeyViewState].ToPago();
                Pagos.PagosID = PagosIdTextBox.Text.ToInt();
                Pagos.FechaRegistro = FechaTextBox.Text.ToDatetime();
                return Pagos;
            }
            protected void AgregarPagoButton_Click(object sender, EventArgs e)
            {
                if (MontoPagarTextBox.Text.ToDecimal() <= 0)
                    return;
                if (!SumarTotalPagos())
                    return;
                Pagos Pago = ViewState[KeyViewState].ToPago();
                Pago.AgregarDetalle(0, Pago.PagosID, AnalisisDropdownList.SelectedValue.ToInt(), MontoPagarTextBox.Text.ToDecimal());
                ViewState[KeyViewState] = Pago;
                this.BindGrid();
                MontoPagarTextBox.Text = string.Empty;
            }
            protected void RemoverDetalleClick_Click(object sender, EventArgs e)
            {
                Pagos Pagos = ViewState[KeyViewState].ToPago();
                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
                Pagos.RemoverDetalle(row.RowIndex);
                ViewState[KeyViewState] = Pagos;
                this.BindGrid();
            }
            protected void AnalisisDropdownList_TextChanged(object sender, EventArgs e)
            {
                if (AnalisisDropdownList.Items.Count > 0)
                {
                    int AnalisisID = AnalisisDropdownList.SelectedValue.ToInt();
                    RepositorioAnalisis repositorio = new RepositorioAnalisis();
                    Analisis analisis = repositorio.Buscar(AnalisisID);
                    RepositorioBase<Pacientes> repositorioPaciente = new RepositorioBase<Pacientes>();
                    Pacientes paciente = repositorioPaciente.Buscar(analisis.PacienteID);
                    PacienteNombreBox.Text = paciente.Nombre;
                    BalanceTextBox.Text = analisis.Balance.ToString();
                    repositorio.Dispose();
                }
            }
            private bool SumarTotalPagos()
            {
                bool paso = false;
                Pagos Pagos = ViewState[KeyViewState].ToPago();
                Analisis analisis = new RepositorioAnalisis().Buscar(AnalisisDropdownList.SelectedValue.ToInt());
                decimal Total = 0;
                Pagos.DetallesPagos.ForEach(x => Total += x.Monto);
                Total += MontoPagarTextBox.Text.ToDecimal();
                paso = Total <= analisis.Monto ? true : false;
                return paso;
            }
            private bool Validar()
            {
                bool paso = true;
                if (AnalisisDropdownList.Items.Count == 0)
                    paso = false;
                if (ViewState[KeyViewState].ToPago().DetallesPagos.Count() == 0)
                    paso = false;
                return paso;
            }
            private bool ExisteEnLaBaseDeDatos()
            {
                RepositorioPago repositorio = new RepositorioPago();
                Pagos pagos = repositorio.Buscar(PagosIdTextBox.Text.ToInt());
                repositorio.Dispose();
            }

            }
        }