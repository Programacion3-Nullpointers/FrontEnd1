<%@ Page Title="" Language="C#" MasterPageFile="MainLayout2.Master"
    AutoEventWireup="true" CodeBehind="ModificarProductos.aspx.cs"
    Inherits="JMQPresentacion.Admin.ModificarProductos" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
       .img-preview {
            width: 100%;
            height: 150px;
            object-fit: contain; 
            object-position: center;
            display: block;
            border: 1px solid #ccc;
            background-color: #fff; 
        }
       .dropdown-custom {
            appearance: none; /* Oculta la flecha nativa */
            -webkit-appearance: none;
            -moz-appearance: none;
            background-color: white;
            border: 1px solid #ccc;
            border-radius: 6px;
            padding: 8px 32px 8px 12px;
            font-size: 14px;
            color: #666;
            background-image: url('data:image/svg+xml;utf8,<svg fill="%23356DFF" height="20" viewBox="0 0 24 24" width="20" xmlns="http://www.w3.org/2000/svg"><path d="M7 10l5 5 5-5z"/></svg>');
            background-repeat: no-repeat;
            background-position: right 10px center;
            background-size: 16px 16px;
            width: 250px;
            margin-bottom: 12px;
        }
        .input-precio {
            width: 200px;
            padding: 8px 10px;
            border-radius: 6px;
            border: 1px solid #ccc;
            font-size: 14px;
            margin-right: 10px;
            margin-bottom: 40px;
        }
        .icon-button {
            text-decoration: none !important;
            font-size: 18px;
            padding: 6px 10px;
        }
        .btn-filtrar-align {
            height: 38px; /* similar a los textbox */
            margin-top: auto;
            margin-bottom: 40px; /* igual que los otros */
            vertical-align: top;
        }
        .alerta-producto {
            display: block;
            margin-top: 20px;
            padding: 10px 20px;
            color: #721c24;
            background-color: #f8d7da;
            border: 1px solid #f5c6cb;
            border-radius: 6px;
            font-weight: bold;
            font-size: 14px;
            max-width: 800px;
        }

    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Productos</h2>
            <button type="button" class="btn-add" onclick="mostrarModal()">➕ Agregar Producto</button>
        </div>

       <!-- 🔍 Buscador con lupa y botón de reset -->
    <div class="input-group mb-3" style="max-width: 600px;">
        <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control" 
            placeholder="Buscar producto..." Style="padding-right: 36px;" />

        <!-- Lupa como botón -->
        <asp:LinkButton ID="btnBuscarNombre" runat="server" OnClick="btnBuscarNombre_Click"
            CssClass="input-group-text icon-button" ToolTip="Buscar">
            🔍
        </asp:LinkButton>

        <!-- Espacio entre botones -->
        <span style="width: 16px;"></span>

        <!-- Botón de lista inicial -->
        <asp:Button ID="btnReset" runat="server" Text="Lista Inicial" CssClass="btn btn-outline-secondary"
            OnClick="btnReset_Click" />
    </div>

    <!-- Autocompletado -->
    <datalist id="listaProductos" runat="server"></datalist>

    <!-- 🧩 Filtros desplegables con estilos -->
    <div class="d-flex flex-wrap gap-2 mb-2" style="max-width: 800px;">
        <asp:DropDownList ID="ddlConDescuentoFiltro" runat="server" CssClass="dropdown-custom">
            <asp:ListItem Text="Con descuento" Value="true" />
            <asp:ListItem Text="Sin descuento" Value="false" />
            <asp:ListItem Text="Todos con/sin descuento" Value="" Selected="True"/>
        </asp:DropDownList>

        <asp:DropDownList ID="ddlCategoriaFiltro" runat="server" CssClass="dropdown-custom">
            <asp:ListItem Text="Todas las categorías" Value="" />
        </asp:DropDownList>

        <asp:DropDownList ID="ddlActivoFiltro" runat="server" CssClass="dropdown-custom">
            <asp:ListItem Text="Todos Activos/Inactivos" Value="" />
            <asp:ListItem Text="Activos" Value="true" />
            <asp:ListItem Text="Inactivos" Value="false" />
        </asp:DropDownList>
    </div>

    <!-- 💰 Filtros de precio y stock con estilos -->
    <div class="d-flex gap-2 align-items-center flex-nowrap" style="max-width: 100%; flex-wrap: nowrap;">
        <asp:TextBox ID="txtPrecioMin" runat="server" CssClass="input-precio" placeholder="Precio mínimo" />
        <asp:TextBox ID="txtPrecioMax" runat="server" CssClass="input-precio" placeholder="Precio máximo" />
        <asp:TextBox ID="txtStockMin" runat="server" CssClass="input-precio" placeholder="Stock mínimo" />
        <asp:TextBox ID="txtStockMax" runat="server" CssClass="input-precio" placeholder="Stock máximo" />
    
        <asp:Button ID="btnAplicarFiltros" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-filtrar-align" 
            OnClick="btnAplicarFiltros_Click" />
    </div>

        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvProductos_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="categoria.nombre" HeaderText="Categoría" />
                 <asp:TemplateField HeaderText="Descuento">
                     <ItemTemplate>
                        <%# ((JMQPresentacion.JMQWS.producto)Container.DataItem).categoria != null ? ((JMQPresentacion.JMQWS.producto)Container.DataItem).categoria.nombre.ToString() : "" %>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="imagen" HeaderText="Imagen (URL)" Visible="false" />
                <asp:BoundField DataField="precio" HeaderText="Precio" />
                <asp:BoundField DataField="stock" HeaderText="Stock" />
                <asp:BoundField DataField="activo" HeaderText="Activo" Visible="false" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn-edit" Text="✏️"/>
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn-delete" Text="🗑️" OnClientClick="return confirm('¿Estás seguro que deseas eliminar este usuario?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Mensaje de alerta -->
    <div style="display: flex; justify-content: center; margin-top: 20px;">
        <asp:Label ID="lblMensaje" runat="server" CssClass="alerta-producto" Visible="false"
            Text="⚠️ Producto no encontrado."></asp:Label>
    </div>

     <!-- Modal Agregar Producto actualizado con nombres de campos proporcionados -->
    <asp:Panel ID="pnlModalAgregar" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModal()">&times;</span>
            <h3>Agregar Producto</h3>

            <asp:TextBox ID="txtNombre" runat="server" CssClass="input-modal" placeholder="Nombre del producto" />
            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="input-modal" />
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="input-modal" placeholder="Descripción" />

            <div class="mb-2">
                <label>Imagen:</label><br />
                <asp:Image ID="Image1" runat="server" CssClass="img-preview" />
                <br />
                <asp:FileUpload ID="fileUpload1" runat="server" CssClass="form-control mt-2" />
                <asp:Button ID="Button1" runat="server" Text="Cargar Imagen"
                            OnClick="btnCargarFotoAgregar_Click" CssClass="btn btn-secondary mt-2" />
            </div>

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
                <%--<asp:TextBox ID="txtCategoriaMod" runat="server" CssClass="form-control" />--%>
                <asp:DropDownList ID="ddlCategoriaMod" runat="server" CssClass="input-modal" />

            </div>

            <!-- Descripción -->
            <div class="mb-2">
                <label for="txtDescripcionMod">Descripción:</label>
                <asp:TextBox ID="txtDescripcionMod" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="3" />
            </div>

            <!-- Imagen con vista previa -->
            <div class="mb-2">
                <label>Imagen:</label><br />
                <asp:Image ID="imgPreviewMod" runat="server" CssClass="img-preview" />
                <br />

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
