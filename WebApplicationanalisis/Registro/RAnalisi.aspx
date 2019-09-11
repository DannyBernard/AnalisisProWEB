<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RAnalisi.aspx.cs" Inherits="WebApplicationanalisis.Registro.RAnalisi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="container">
		<div class="panel panel-primary">
			<div class="panel-heading">Registro de Analisis</div>

			<div class="panel-body">
				<div class="form-horizontal col-md-12" role="form">

					<div class="form-group">
						<label for="PresupuestoTextBox" class="col-md-3 control-label input-sm">ID</label>
						<div class="col-md-8">
							<asp:TextBox ID="idTextBox" CssClass=" form-control " placeholder="ID" runat="server" Height="2.5em"></asp:TextBox>
							  <asp:RequiredFieldValidator ID="RFVId" ValidationGroup="Buscar" ControlToValidate="AnalisisIdTextBox" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
							<asp:Button ID="buscarButton" CssClass=" form-control btn btn-primary" runat="server" Text="Buscar" OnClick="buscarButton_Click" />
							<!--
							-->
						</div>
					</div>

					<div class="form-group">
						<label for="descripcionTextBox" class="col-md-3 control-label input-sm">Descripcion: </label>
						<div class="col-md-8">
							<asp:TextBox ID="descripcionTextBox" CssClass=" form-control" placeholder="Descripción" runat="server" Height="2.5em"></asp:TextBox>
				 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Guardar" ControlToValidate="descripcionTextBox" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

						</div>
						<div class="form-group">
							<label for="PacienteTextBox" class="col-md-3 control-label input-sm">Paciente: </label>
							<div class="col-md-8">
								<asp:TextBox ID="PaceinteTextBox" CssClass=" form-control" placeholder="Paciente" runat="server" Height="2.5em"></asp:TextBox>
	 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Guardar" ControlToValidate="PaceinteTextBox" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

							</div>
						</div>

						<div class="form-group">
							<label for="DropDownList" class="col-md-3 control-label input-sm">Tipo Analisis</label>
							<div class="col-md-8">
								<asp:DropDownList ID="tipoDropDownList" CssClass=" form-control dropdown" AppendDataBoundItems="true" runat="server" Height="2.5em">
									<asp:ListItem Text="propro" />
								</asp:DropDownList>
					  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="tipoDropDownList" ValidationGroup="Guardar" SetFocusOnError="true" runat="server" ErrorMessage="Cliente: Seleccione un cliente" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>


							</div>
							<div class="col-md-6 col-md-offset-0">

								<asp:LinkButton ID="AddLinkButton" CssClass="btn btn-primary " runat="server" OnClick="AddLinkButton_Click">
                          <span class="fas fa-plus"></span> Agregar
                         
								</asp:LinkButton>
							</div>

						</div>


						<div class="row">
							<asp:GridView ID="DetalleGridView" CssClass=" col-md-offset-4 col-sm-offset-4" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="244px" AutoGenerateColumns="true">
								<AlternatingRowStyle BackColor="White" />

								<EditRowStyle BackColor="#2461BF" />
								<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
								<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
								<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
								<RowStyle BackColor="#EFF3FB" />
								<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
								<SortedAscendingCellStyle BackColor="#F5F7FB" />
								<SortedAscendingHeaderStyle BackColor="#6D95E1" />
								<SortedDescendingCellStyle BackColor="#E9EBEF" />
								<SortedDescendingHeaderStyle BackColor="#4870BE" />
							</asp:GridView>
						</div>
						<div class="col-md-6 col-md-offset-0">

							<asp:LinkButton ID="RemoveLinkButton" CssClass="btn btn-danger " runat="server" OnClick="RemoveLinkButton_Click">
                          <span class="fas fa-plus"></span> Remove
                         
							</asp:LinkButton>
						</div>
					</div>
				</div>

				<div class="panel-footer">
					<div class="text-center">
						<div class="form-group" style="display: inline-block">
							<asp:Button ID="LimpiarButton" CssClass=" col-md-4 col-sm-4 btn btn-primary" runat="server" Text="Limpiar" Height="2.5em" Width="10em" OnClick="LimpiarButton_Click" />
							<asp:Button ID="GuardarButton" CssClass="col-md-4 col-sm-4 btn btn-success" runat="server" Text="Guardar" Height="2.5em" Width="10em" OnClick="GuardarButton_Click" />
							<asp:Button ID="EliminarButton" CssClass="col-md-4 col-sm-4 btn btn-danger" runat="server" Text="Eliminar" Height="2.5em" Width="10em" OnClick="EliminarButton_Click" />
							<!--
						-->
						</div>
					</div>
				</div>
			</div>
		</div>
</asp:Content>
