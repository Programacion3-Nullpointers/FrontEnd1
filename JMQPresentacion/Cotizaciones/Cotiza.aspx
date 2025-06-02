<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Cotiza.aspx.cs" Inherits="JMQPresentacion.Pedidos.Cotiza" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4" style="max-width: 600px;">
        <h2 class="h4 text-center mb-3">Cotización de Productos</h2>
        <div class="card">
            <div class="card-body">
                <div class="mb-3">
                    <label class="form-label">Nombre del Producto:</label>
                    <asp:TextBox ID="txtProducto" runat="server" CssClass="form-control" placeholder="Ejemplo: Taladro"/>
                </div>
                <div class="mb-3">
                    <label class="form-label">Cantidad:</label>
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"/>
                </div>
                <div class="mb-3">
                    <label class="form-label">Precio Unitario:</label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number"/>
                </div>
                <div class="text-center">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar a Cotización" CssClass="btn btn-primary" OnClick="btnAgregar_Click"/>
                </div>

                <!-- Lista de productos cotizados -->
                <div class="mt-4">
                    <asp:GridView ID="gvCotizacion" runat="server" CssClass="table table-bordered"/>
                </div>

                <!-- Total de la cotización -->
                <div class="mt-3 text-end">
                    <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
