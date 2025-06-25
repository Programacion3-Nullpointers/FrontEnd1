<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MainLayout2.Master" AutoEventWireup="true" CodeBehind="ModificarDescuentos.aspx.cs" Inherits="JMQPresentacion.Admin.ModificarDescuentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
    <div class="table-header">
        <h2>Gestión de Descuentos</h2>
    </div>

    <asp:GridView ID="gvDescuentos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvDescuentos_RowCommand">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="numDescuento" HeaderText="Porcentaje" />

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn-edit" Text="✏️"/>
                    <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn-delete" Text="🗑️" OnClientClick="return confirm('¿Estás seguro que deseas eliminar este usuario?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<!-- Modal -->
<asp:Panel ID="pnlModalAgregar" runat="server" CssClass="modal" Style="display: none;">
    <div class="modal-content">
        <span class="cerrar" onclick="cerrarModal()">&times;</span>
        <h3>Modificar Usuario</h3>

        <asp:TextBox ID="txtnumDescuento" runat="server" CssClass="input-modal" placeholder="Descuento" />
        <%--<asp:TextBox ID="txtactivo" runat="server" CssClass="input-modal" placeholder="activo" />--%>
        
        <asp:Button ID="btnGuardarDescuento" runat="server" Text="Guardar" CssClass="btn-add" OnClick="btnGuardarDescuento_Click" />
    </div>
</asp:Panel>

<!-- Modal Modificar Descuento -->
<asp:Panel ID="pnlModalModificar" runat="server" CssClass="modal" Style="display: none;">
    <div class="modal-content">
        <span class="cerrar" onclick="cerrarModalModificar()">&times;</span>
        <h3>Modificar Usuario</h3>

        <asp:HiddenField ID="hfIdDescuento" runat="server" />

        <asp:TextBox ID="txtDescuentoMod" runat="server" CssClass="input-modal" placeholder="Descuento" />
        <%--<asp:TextBox ID="txtActivoMod" runat="server" CssClass="input-modal" placeholder="Activo" />--%>
        
        <asp:Button ID="btnActualizarDescuento" runat="server" Text="Actualizar" CssClass="btn-edit" OnClick="btnActualizarDescuento_Click" />
    </div>
</asp:Panel>
    <script type="text/javascript">
        function mostrarModalModificar() {
            document.getElementById('<%= pnlModalModificar.ClientID %>').style.display = 'block';
        }

        function cerrarModalModificar() {
            document.getElementById('<%= pnlModalModificar.ClientID %>').style.display = 'none';
        }
    </script>
</asp:Content>
