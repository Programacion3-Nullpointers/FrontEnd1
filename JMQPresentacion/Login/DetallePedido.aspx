<%@ Page Title="" Language="C#" MasterPageFile="~/Login/Login.Master" AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs" Inherits="JMQPresentacion.Login.DetallePedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">Detalle de Pedido</h2>

        <asp:GridView ID="gvDetalles" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="producto.nombre" HeaderText="Producto" />
            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
            <asp:BoundField DataField="precio_unitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" />

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
