<%@ Page Title="" Language="C#" MasterPageFile="MainLayout2.Master" AutoEventWireup="true" CodeBehind="CotizacionesAdmin.aspx.cs" Inherits="JMQPresentacion.Cotizaciones.Cotizaciones" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-header {
            display: flex;
            display: flex;
            justify-content: space-between;
            justify-content: space-between;
            align-items: center;
            align-items: center;
            margin-bottom: 20px;
            margin-bottom: 20px;
            flex-wrap: wrap;
            gap: 10px;
        }

        @media (max-width: 768px) {
            .content {
                margin-left: 0;
            }

            .table-header {
                flex-direction: column;
                align-items: flex-start;
            }

            .modal-content {
                width: 95%;
                margin: 40px auto;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Cotizaciones</h2>
            <button type="button" class="btn-add" onclick="mostrarModal(false)">➕ Agregar Cotización</button>
        </div>

        <asp:GridView ID="gvCotizaciones" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCotizaciones_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombreUsuario" HeaderText="Usuario" />
                <asp:BoundField DataField="estado" HeaderText="Estado" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn-edit" Text="✏️" />
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn-delete" Text="🗑️" OnClientClick="return confirm('¿Eliminar esta cotización?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Modal Cotización -->
    <asp:Panel ID="pnlModalAgregar" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModal()">&times;</span>
            <h3 id="modalTitle">Agregar Cotización</h3>

            <!-- Solo se mostrará en modo Agregar -->
            <div class="grupo-datos">
                <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="input-modal" placeholder="Nombre de Usuario" />
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="input-modal" placeholder="Correo Electrónico" />
                <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="input-modal" placeholder="Razón Social" />
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="input-modal" placeholder="Dirección" />
                <asp:TextBox ID="txtRUC" runat="server" CssClass="input-modal" placeholder="RUC" />
            </div>

            <!-- Siempre editable -->
            <asp:TextBox ID="txtEstado" runat="server" CssClass="input-modal" placeholder="Estado (Ej: Pendiente, Aprobada)" />

            <asp:Button ID="btnGuardarCotizacion" runat="server" Text="Guardar" CssClass="btn-add" OnClick="btnGuardarCotizacion_Click" />
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function mostrarModal(esEdicion) {
            document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'block';

            document.getElementById('modalTitle').innerText = esEdicion
                ? "Editar Estado de Cotización"
                : "Agregar Cotización";

            var datosUsuario = document.querySelector('.grupo-datos');
            if (datosUsuario) {
                datosUsuario.style.display = esEdicion ? 'none' : 'block';
            }
        }

        function cerrarModal() {
            document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'none';
        }
    </script>
</asp:Content>