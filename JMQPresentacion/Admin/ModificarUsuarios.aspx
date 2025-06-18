<%@ Page Title="" Language="C#" MasterPageFile="MainLayout2.Master" AutoEventWireup="true" CodeBehind="ModificarUsuarios.aspx.cs" Inherits="JMQPresentacion.Usuarios.ModificarUsuarios" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Usuarios</h2>
        </div>

        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" OnRowCommand="gvUsuarios_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="tipoUsuario" HeaderText="Tipo de Usuario" /> 
                <asp:BoundField DataField="correo" HeaderText="Correo" />
                <asp:BoundField DataField="razonsocial" HeaderText="Razón Social" />
                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="RUC" HeaderText="RUC" />

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

            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="input-modal" placeholder="Nombre de Usuario" />
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="input-modal" placeholder="Correo" />
            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="input-modal" placeholder="Razón Social" />
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="input-modal" placeholder="Dirección" />
            <asp:TextBox ID="txtRUC" runat="server" CssClass="input-modal" placeholder="RUC" />

            <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn-add" OnClick="btnGuardarUsuario_Click" />
        </div>
    </asp:Panel>

    <!-- Modal Modificar Usuario -->
    <asp:Panel ID="pnlModalModificar" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModalModificar()">&times;</span>
            <h3>Modificar Usuario</h3>

            <asp:HiddenField ID="hfIdUsuario" runat="server" />

            <asp:TextBox ID="txtNombreUsuarioMod" runat="server" CssClass="input-modal" placeholder="Nombre de Usuario" />
            <asp:TextBox ID="txtCorreoMod" runat="server" CssClass="input-modal" placeholder="Correo" />
            <asp:TextBox ID="txtRazonSocialMod" runat="server" CssClass="input-modal" placeholder="Razón Social" />
            <asp:TextBox ID="txtDireccionMod" runat="server" CssClass="input-modal" placeholder="Dirección" />
            <asp:TextBox ID="txtRUCMod" runat="server" CssClass="input-modal" placeholder="RUC" />

            <asp:Button ID="btnActualizarUsuario" runat="server" Text="Actualizar" CssClass="btn-edit" OnClick="btnActualizarUsuario_Click" />
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