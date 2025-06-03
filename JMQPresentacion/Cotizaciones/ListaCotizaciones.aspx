<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="ListaCotizaciones.aspx.cs" Inherits="JMQPresentacion.Cotizaciones.ListaCotizaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4">
        <h2 class="text-center mb-4">Mis Cotizaciones</h2>
        
        <!-- 🔹 Panel vacío (se muestra si no hay cotizaciones) -->
        <asp:Panel ID="pnlSinCotizaciones" runat="server" Visible="false" CssClass="text-center py-5 border rounded">
            <i class="fas fa-file-alt fa-3x mb-3" style="color: #7f8c8d;"></i>
            <h4 style="color: #2c3e50;">No hay cotizaciones registradas</h4>
            <p class="text-muted">¿Necesitas un producto especial? <asp:HyperLink runat="server" NavigateUrl="~/Cotizar.aspx" CssClass="text-primary">Solicita una cotización aquí</asp:HyperLink></p>
        </asp:Panel>

        <!-- 🔹 Lista de cotizaciones (se muestra si hay datos) -->
        <asp:Repeater ID="rptCotizaciones" runat="server" Visible="false">
            <HeaderTemplate>
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead class="table-light">
                            <tr>
                                <th>ID</th>
                                <th>Fecha</th>
                                <th>Producto</th>
                                <th>Estado</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                            <tr>
                                <td><%# Eval("IdCotizacion") %></td>
                                <td><%# Eval("Fecha", "{0:dd/MM/yyyy}") %></td>
                                <td><%# Eval("NombreProducto") %></td>
                                <td><span class='badge <%# GetEstadoCssClass(Eval("Estado")) %>'><%# Eval("Estado") %></span></td>
                                <td>
                                    <asp:LinkButton runat="server" CssClass="btn btn-sm btn-outline-primary" CommandArgument='<%# Eval("IdCotizacion") %>' OnClick="VerDetalle_Click">
                                        <i class="fas fa-eye"></i> Ver
                                    </asp:LinkButton>
                                </td>
                            </tr>
            </ItemTemplate>
            <FooterTemplate>
                        </tbody>
                    </table>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
