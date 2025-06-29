<%@ Page Title="" Language="C#" MasterPageFile="MainLayout2.Master" AutoEventWireup="true" CodeBehind="ModificarOrdenes.aspx.cs" Inherits="JMQPresentacion.Admin.ModificarOrdenes" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- ScriptManager necesario para ejecutar scripts desde el servidor -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Órdenes de Venta</h2>
        </div>

        <!-- Filtros -->
        <div class="container mb-4">
            <div class="row gy-2 gx-3 align-items-end">

                <div class="col-md-3">
                    <label for="ddlEstado" class="form-label fw-bold">Estado:</label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todos" Value="" />
                        <asp:ListItem Text="pendiente" Value="pendiente" />
                        <asp:ListItem Text="pagado" Value="pagado" />
                        <asp:ListItem Text="enviado" Value="enviado" />
                        <asp:ListItem Text="entregado" Value="entregado" />
                        <asp:ListItem Text="cancelado" Value="cancelado" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <label for="txtBuscarUsuario" class="form-label fw-bold">Buscar por ID de Usuario:</label>
                    <asp:TextBox ID="txtBuscarUsuario" runat="server" CssClass="form-control" placeholder="Ingrese ID" />
                </div>

                <div class="col-md-2">
                    <label for="ddlActivo" class="form-label fw-bold">Activo:</label>
                    <asp:DropDownList ID="ddlActivo" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Sí" Value="1" />
                        <asp:ListItem Text="No" Value="0" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">
                    <label for="txtFechaDesde" class="form-label fw-bold">Fecha Desde:</label>
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-2">
                    <label for="txtFechaHasta" class="form-label fw-bold">Fecha Hasta:</label>
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="Date" />
                </div>

                <div class="col-md-2 d-grid">
                    <asp:Button ID="btnFiltrar" runat="server" CssClass="btn btn-primary" Text="Filtrar" OnClick="btnFiltrar_Click" />
                </div>

                <div class="col-md-2 d-grid">
                    <asp:Button ID="btnResetFiltros" runat="server" CssClass="btn btn-secondary" Text="Reset" OnClick="btnResetFiltros_Click" />
                </div>

            </div>
        </div>

        <!-- Mensaje -->
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-info" Visible="false"></asp:Label>

        <!-- Tabla principal -->
        <asp:GridView ID="gvOrdenes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnRowCommand="gvOrdenes_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID Orden" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Eval("estado_compra").ToString().ToLower()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fecha_orden" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Usuario">
                    <ItemTemplate>
                        <%# Eval("usuario.nombreUsuario") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Activo">
                    <ItemTemplate>
                        <%# (bool)Eval("activo") ? "Sí" : "No" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnVer" runat="server" CommandName="Ver" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-outline-info btn-sm" Text="Ver" />
                        <asp:Button ID="btnCambiarEstado" runat="server" CommandName="CambiarEstado" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-outline-warning btn-sm" Text="Estado" />
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-outline-danger btn-sm" Text="Eliminar" OnClientClick="return confirm('¿Deseas eliminar esta orden?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Modal Ver Detalles -->
    <div class="modal fade" id="modalVerDetalles" tabindex="-1" aria-labelledby="modalVerDetallesLabel" aria-hidden="true">
        <div class="modal-dialog" style="max-width: 95% !important; width: 95%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalVerDetallesLabel">Detalles de la Orden</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="gvDetallesOrden" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-bordered" style="min-width: 100%; font-size: 1rem;">
                        <Columns>
                            <asp:BoundField DataField="producto.nombre" HeaderText="Producto" />
                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                            <asp:BoundField DataField="precio_unitario" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

        <!-- Modal Cambiar Estado -->
        <div class="modal fade" id="modalCambiarEstado" tabindex="-1" aria-labelledby="modalCambiarEstadoLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalCambiarEstadoLabel">Cambiar Estado de Orden</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hfIdOrdenEstado" runat="server" />
                        <div class="mb-3">
                            <label for="ddlNuevoEstado" class="form-label">Nuevo Estado:</label>
                            <asp:DropDownList ID="ddlNuevoEstado" runat="server" CssClass="form-select">
                                <asp:ListItem Text="pendiente" Value="pendiente" />
                                <asp:ListItem Text="pagado" Value="pagado" />
                                <asp:ListItem Text="enviado" Value="enviado" />
                                <asp:ListItem Text="entregado" Value="entregado" />
                                <asp:ListItem Text="cancelado" Value="cancelado" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnGuardarEstado" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardarEstado_Click" />
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Scripts de Bootstrap y jQuery (necesarios para los modales) -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

</asp:Content>
