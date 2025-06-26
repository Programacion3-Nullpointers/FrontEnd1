<%@ Page Title="" Language="C#" MasterPageFile="MainLayout2.Master"
    AutoEventWireup="true" CodeBehind="ModificarProductos.aspx.cs"
    Inherits="JMQPresentacion.Admin.ModificarProductos" %><asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Productos</h2>
            <button type="button" class="btn-add" onclick="mostrarModal()">➕ Agregar Producto</button>
        </div>

        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvProductos_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="categoria.nombre" HeaderText="Categoría" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="imagen" HeaderText="Imagen (URL)" Visible="false" />
                <asp:BoundField DataField="precio" HeaderText="Precio" />
                <asp:BoundField DataField="stock" HeaderText="Stock" />
                <asp:BoundField DataField="activo" HeaderText="Activo" Visible ="false" />
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
            <h3>Agregar Producto</h3>

            <asp:TextBox ID="txtNombre" runat="server" CssClass="input-modal" placeholder="Nombre del producto" />
            <asp:TextBox ID="txtCategoriaNombre" runat="server" CssClass="input-modal" placeholder="Categoría" />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-modal" placeholder="Descripción" />
            <asp:TextBox ID="txtImagen" runat="server" CssClass="input-modal" placeholder="Imagen (URL)" />
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="input-modal" placeholder="Precio" />
            <asp:TextBox ID="txtStock" runat="server" CssClass="input-modal" placeholder="Stock" />
            <asp:Button ID="btnGuardarProducto" runat="server" Text="Guardar" CssClass="btn-add" OnClick="btnGuardarProducto_Click" />
        </div>
    </asp:Panel>

    <!-- Modal Modificar Producto -->
    <asp:Panel ID="pnlModalModificar" runat="server" CssClass="modal" Style="display: none;">
            <div class="modal-content">
                <span class="cerrar" onclick="cerrarModalModificar()">&times;</span>
                <h3 class="mb-3">Modificar Producto</h3>

                <asp:HiddenField ID="hfIdProd" runat="server" />

                <!-- Nombre del producto -->
                <div class="mb-2">
                    <label for="txtNombreMod">Nombre del producto:</label>
                    <asp:TextBox ID="txtNombreMod" runat="server" CssClass="form-control" />
                </div>

                <!-- Categoría -->
                <div class="mb-2">
                    <label for="txtCategoriaMod">Categoría:</label>
                    <asp:TextBox ID="txtCategoriaMod" runat="server" CssClass="form-control" />
                </div>

                <!-- Descripción -->
                <div class="mb-2">
                    <label for="txtDescripcionMod">Descripción:</label>
                    <asp:TextBox ID="txtDescripcionMod" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" />
                </div>

                <!-- Imagen con vista previa -->
                <div class="mb-2">
                    <label>Imagen:</label><br />
                    <asp:Image ID="imgPreviewMod" runat="server" Width="100%" Height="150px" Style="object-fit: cover; border: 1px solid #ccc;" />
                    <br />
                    <!--<asp:FileUpload ID="fuImagen" runat="server" CssClass="input-modal" />-->

                    <!-- NUEVOS CONTROLES PARA CARGAR Y PREVISUALIZAR -->
                    <asp:FileUpload ID="fileUploadFotoProducto" runat="server" CssClass="form-control mt-2" />
                    <asp:Button ID="btnCargarFoto" runat="server" Text="Cargar Imagen" OnClick="btnCargarFoto_Click" CssClass="btn btn-secondary mt-2" />
                </div>

                <!-- Precio -->
                <div class="mb-2">
                    <label for="txtPrecioMod">Precio:</label>
                    <asp:TextBox ID="txtPrecioMod" runat="server" CssClass="form-control" />
                </div>

                <!-- Stock -->
                <div class="mb-2">
                    <label for="txtStockMod">Stock:</label>
                    <asp:TextBox ID="txtStockMod" runat="server" CssClass="form-control" />
                </div>

                <!-- Activo (Checkbox) -->
                <div class="form-check my-2">
                    <asp:CheckBox ID="chkActivoMod" runat="server" CssClass="form-check-input" />
                    <label class="form-check-label" for="chkActivoMod">Activo</label>
                </div>

                <!-- Botón Guardar -->
                <asp:Button ID="btnActualizarProducto" runat="server" Text="Actualizar"
                            CssClass="btn btn-primary w-100 mt-3"
                            OnClick="btnActualizarProducto_Click" />
            </div>
        </asp:Panel>



        <script type="text/javascript">
            function mostrarModal() {
                document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'block';
            }
            function cerrarModal() {
                document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'none';
            }
            function mostrarModalModificar() {
                document.getElementById('<%= pnlModalModificar.ClientID %>').style.display = 'block';
            }

            function cerrarModalModificar() {
                document.getElementById('<%= pnlModalModificar.ClientID %>').style.display = 'none';
            }
        </script>
</asp:Content>