<%@ Page Title="" Language="C#" MasterPageFile="~/Registro/Site1.Master" AutoEventWireup="true" CodeBehind="RAnalisis.aspx.cs" Inherits="WebApplicationanalisis.Registro.RAnalisis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="panel panel-primary">
	<div class="panel-heading">Registro de analisis</div>

	<div class="form-horizontal col-md-12 role="form">
		<%--AnalisisID--%>
		<div class="form-gruop">
			<label for="IDtexbox" class="col-md-3 control-label input-sm">ID:</label>
			<div class="cold-md-1 col-sm-2 col-xs-4">
				<asp:TextBox ID="IDTextBOx" runat="server" ReadOnly="false" placeholder="0" class="form-control input-sm"></asp:TextBox>
			</div>
			<asp:LinkButton ID="BuscarLinkButton" runat="server" CssClass="btn btn-primary"> <spam class="glyphicon glyphicon-search"></spam> Buscar</asp:LinkButton>
		</div>
	</div>
</div>
</asp:Content>
