<%@ Page Title="" Language="C#" MasterPageFile="MainLayout2.Master" AutoEventWireup="true" CodeBehind="ModificarUsuarios.aspx.cs" Inherits="JMQPresentacion.Usuarios.ModificarUsuarios" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .btn-filter, .btn-reset {
            margin-left: 10px;
            padding: 6px 14px;
            border-radius: 6px;
            border: none;
            background-color: #2c3e50;
            color: white;
            cursor: pointer;
        }

        .btn-reset {
            background-color: #7f8c8d;
        }

        .filters label {
            margin-right: 6px;
            font-weight: bold;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Usuarios</h2>
        </div>
    <!-- 🔍 Filtros y buscador en una sola fila -->
    <div class="container mb-4">
        <div class="row gy-2 gx-3 align-items-end">

            <!-- Buscador -->
            <div class="col-md-5">
                <label for="txtBuscar" class="form-label fw-bold">Buscar por Usuario, DNI o RUC:</label>
                <div class="input-group">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Ingrese nombre, DNI o RUC" />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-dark" OnClick="btnBuscar_Click" />
                </div>
            </div>

            <!-- Filtro Tipo de Entidad -->
            <div class="col-md-2">
                <label for="ddlTipoEntidad" class="form-label fw-bold">Tipo de Entidad:</label>
                <asp:DropDownList ID="ddlTipoEntidad" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Todos" Value="" />
                    <asp:ListItem Text="Empresa" Value="empresa" />
                    <asp:ListItem Text="Persona natural" Value="persona" />
                </asp:DropDownList>
            </div>

            <!-- Filtro Activo -->
            <div class="col-md-2">
                <label for="ddlActivo" class="form-label fw-bold">Activo:</label>
                <asp:DropDownList ID="ddlActivo" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Todos" Value="" />
                    <asp:ListItem Text="Activo" Value="true" />
                    <asp:ListItem Text="Inactivo" Value="false" />
                </asp:DropDownList>
            </div>

            <!-- Botones -->
            <div class="col-md-1 d-grid">
                <asp:Button ID="btnFiltrar" runat="server" CssClass="btn btn-primary" Text="Filtrar" OnClick="btnFiltrar_Click" />
            </div>
            <div class="col-md-1 d-grid">
                <asp:Button ID="btnResetFiltros" runat="server" CssClass="btn btn-secondary" Text="Reset" OnClick="btnResetFiltros_Click" />
            </div>
        </div>
    </div>


        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" OnRowCommand="gvUsuarios_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="tipoUsuario" HeaderText="Tipo de Usuario" /> 
                <asp:BoundField DataField="correo" HeaderText="Correo" />


                <asp:TemplateField HeaderText="RUC">
                    <ItemTemplate>
                        <%# string.IsNullOrEmpty(Eval("RUC") as string) ? "—" : Eval("RUC") %>
                    </ItemTemplate>
                </asp:TemplateField>

             
                <asp:TemplateField HeaderText="DNI">
                    <ItemTemplate>
                        <%# string.IsNullOrEmpty(Eval("dni") as string) ? "—" : Eval("dni") %>
                    </ItemTemplate>
                </asp:TemplateField>

              
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn-edit" Text="✏️"/>
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn-delete" Text="🗑️" OnClientClick="return confirm('¿Estás seguro que deseas eliminar este usuario?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Modal Agregar Usuario -->
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
