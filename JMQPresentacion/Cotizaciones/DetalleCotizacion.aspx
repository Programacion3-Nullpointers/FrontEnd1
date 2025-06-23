<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="DetalleCotizacion.aspx.cs" Inherits="JMQPresentacion.Cotizaciones.DetalleCotizacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <div class="container mt-4">
        <h2 class="mb-3">Detalle de Cotización</h2>

        <div class="mb-3">
            <strong>Estado:</strong> <asp:Label ID="lblEstado" runat="server" />
        </div>

        <asp:GridView ID="gvProductos" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="descripcion" HeaderText="Producto" />
                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="precioCotizado" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblError" runat="server" CssClass="text-danger" />
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
