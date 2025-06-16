<%@ Page Title="" Language="C#" MasterPageFile="MainLayout2.Master" AutoEventWireup="true" CodeBehind="ModificarProductoAdmin.aspx.cs" Inherits="JMQPresentacion.Admin.ModificarProductoAdmin" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:HiddenField ID="hfIdProd" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Productos</h2>
            <button type="button" class="btn-add" onclick="mostrarModal()">➕ Agregar Producto</button>
        </div>

        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvProductos_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="precio" HeaderText="Precio (S/.)" DataFormatString="{0:N2}" />
                <asp:BoundField DataField="stock" HeaderText="Stock" />
                <asp:BoundField DataField="categoria.nombre" HeaderText="Categoría" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn-edit" Text="✏️" />
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn-delete" Text="🗑️" OnClientClick="return confirm('¿Estás seguro que deseas eliminar este producto?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Modal Agregar -->
    <asp:Panel ID="pnlModalAgregar" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModal()">&times;</span>
            <h3>Agregar Producto</h3>

            <asp:TextBox ID="txtNombre" runat="server" CssClass="input-modal" placeholder="Nombre del producto" />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-modal" placeholder="Descripción" TextMode="MultiLine" Rows="3" />
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="input-modal" placeholder="Precio" />
            <asp:TextBox ID="txtStock" runat="server" CssClass="input-modal" placeholder="Stock" />
            <asp:TextBox ID="txtCategoriaNombre" runat="server" CssClass="input-modal" placeholder="Categoría" />
            <asp:FileUpload ID="fileUploadImgProducto" runat="server" CssClass="input-modal" />
            <asp:Image ID="imgProducto" runat="server" CssClass="img-fluid img-thumbnail" Height="200" Width="100%" Visible="false" />

            <asp:Button ID="btnGuardarProducto" runat="server" Text="Guardar" CssClass="btn-add" OnClick="btnGuardarProducto_Click" />
        </div>
    </asp:Panel>

    <!-- Modal Modificar -->
    <!-- Modal Modificar Producto (estilo similar al de RegistrarEvento) -->
    <asp:Panel ID="pnlModalModificar" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content container">
            <span class="cerrar" onclick="cerrarModalModificar()">&times;</span>
            <h3 class="mb-3">Modificar Producto</h3>
            <div class="row">
                <!-- Imagen -->
                <div class="col-md-6">
                    <asp:Label ID="lblImgProductoMod" runat="server" Text="Imagen del Producto:" CssClass="col-form-label fw-bold"></asp:Label>
                    <asp:Image ID="imgProductoMod" runat="server" CssClass="img-fluid img-thumbnail mb-2" Height="200" Width="100%" Visible="false" />
                    <asp:FileUpload ID="fileUploadImgProductoMod" runat="server" CssClass="form-control mb-3" />
                </div>

                <!-- Campos de texto -->
                <div class="col-md-6">
                    <div class="mb-2">
                        <asp:Label ID="lblNombreMod" runat="server" Text="Nombre del Producto:" CssClass="col-form-label fw-bold" />
                        <asp:TextBox ID="txtNombreMod" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblDescripcionMod" runat="server" Text="Descripción:" CssClass="col-form-label fw-bold" />
                        <asp:TextBox ID="txtDescripcionMod" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblPrecioMod" runat="server" Text="Precio (S/.):" CssClass="col-form-label fw-bold" />
                        <asp:TextBox ID="txtPrecioMod" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblStockMod" runat="server" Text="Stock Disponible:" CssClass="col-form-label fw-bold" />
                        <asp:TextBox ID="txtStockMod" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-2">
                        <asp:Label ID="lblCategoriaNombreMod" runat="server" Text="Categoría:" CssClass="col-form-label fw-bold" />
                        <asp:TextBox ID="txtCategoriaNombreMod" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>

            <!-- Botón Guardar -->
            <div class="text-end mt-3">
                <asp:Button ID="btnActualizarProducto" runat="server" Text="Actualizar" CssClass="btn btn-primary" OnClick="btnActualizarProducto_Click" />
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function mostrarModalModificar() {
            document.getElementById('<%= pnlModalModificar.ClientID %>').style.display = 'block';
        }

        function cerrarModalModificar() {
            document.getElementById('<%= pnlModalModificar.ClientID %>').style.display = 'none';
        }

        function cerrarModal() {
            document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'none';
        }
    </script>
</asp:Content>
