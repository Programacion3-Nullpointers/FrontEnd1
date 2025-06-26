<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MainLayout2.Master" AutoEventWireup="true" CodeBehind="VerPedidosAdmin.aspx.cs" Inherits="JMQPresentacion.Admin.VerPedidosAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <div class="table-header">
            <h2>Historial de Ordenes de Venta</h2>
        </div>

        <asp:HiddenField ID="hfIdUsuario" runat="server" />

        <asp:GridView ID="gvOrdenesVenta" runat="server" AutoGenerateColumns="False" OnRowCommand="gvOrdenesVenta_RowCommand" >
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID Orden" />
                <asp:BoundField DataField="estado_compra" HeaderText="Estado de Compra" />
                <asp:BoundField DataField="fecha_orden" HeaderText="Fecha de Orden" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="EditarEstado" CommandArgument='<%# Eval("id") %>' CssClass="btn-edit" Text="Editar estado" UseSubmitBehavior="False"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblNoPedidos" runat="server" Text="No hay pedidos realizados." Visible="false" CssClass="message-info"></asp:Label>
    </div>

    <asp:Panel ID="pnlModalEditarEstado" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModalEditarEstado()">&times;</span>
            <h3>Editar Estado de Orden</h3>

            <asp:HiddenField ID="hfIdOrdenVentaEditar" runat="server" />

            <label for="<%= ddlEstadoOrden.ClientID %>">Nuevo Estado:</label>
            <asp:DropDownList ID="ddlEstadoOrden" runat="server" CssClass="input-modal">
                <%-- Los elementos se llenarán desde el code-behind --%>
            </asp:DropDownList>
            
            <asp:Button ID="btnGuardarEstado" runat="server" Text="Guardar Estado" CssClass="btn-add" OnClick="btnGuardarEstado_Click"  />
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function mostrarModalEditarEstado() {
            document.getElementById('<%= pnlModalEditarEstado.ClientID %>').style.display = 'block';
        }

        function cerrarModalEditarEstado() {
            document.getElementById('<%= pnlModalEditarEstado.ClientID %>').style.display = 'none';
        }
    </script>

</asp:Content>
