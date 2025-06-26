<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="VerPedidos.aspx.cs" Inherits="JMQPresentacion.Login.VerPedidosaspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="content">
        <div class="table-header">
            <h2>Historial de Ordenes de Pedidos</h2>
        </div>

        <asp:HiddenField ID="hfIdUsuario" runat="server" />

        <asp:GridView ID="gvOrdenesVenta" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID Orden" />
                <asp:BoundField DataField="estado_compra" HeaderText="Estado de Compra" />
                <asp:BoundField DataField="fecha_orden" HeaderText="Fecha de Orden" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblNoPedidos" runat="server" Text="No hay pedidos realizados." Visible="false" CssClass="message-info"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
