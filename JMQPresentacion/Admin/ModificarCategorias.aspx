<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MainLayout2.Master" AutoEventWireup="true" CodeBehind="ModificarCategorias.aspx.cs" Inherits="JMQPresentacion.Admin.ModificarCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Categorias</h2>
            <button type="button" class="btn-add" onclick="mostrarModal()">➕ Agregar Categoria</button>
        </div>

        <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCategorias_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre Categoria" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                 <asp:TemplateField HeaderText="Descuento">
                     <ItemTemplate>
                        <%# ((JMQPresentacion.JMQWS.categoria)Container.DataItem).descuento != null ? ((JMQPresentacion.JMQWS.categoria)Container.DataItem).descuento.numDescuento.ToString() : "" %>
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



    
    <!-- Modal -->
    <asp:Panel ID="pnlModalAgregar" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModal()">&times;</span>
            <h3>Modificar Categoria</h3>

            <asp:TextBox ID="txtnombre" runat="server" CssClass="input-modal" placeholder="Categoria" />
            <asp:TextBox ID="txtdescripcion" runat="server" CssClass="input-modal" placeholder="Descripcion" />
            <%--<asp:TextBox ID="txtDescuento" runat="server" CssClass="input-modal" placeholder="Categoria" />--%>
            <asp:DropDownList ID="ddlDescuento" runat="server" CssClass="input-modal" />

            <asp:Button ID="btnGuardarCategoria" runat="server" Text="Guardar" CssClass="btn-add" OnClick="btnGuardarCategoria_Click" />
        </div>
    </asp:Panel>

    <!-- Modal Modificar Categoria -->
    <asp:Panel ID="pnlModalModificar" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModalModificar()">&times;</span>
            <h3>Modificar Categoria</h3>

            <asp:HiddenField ID="hfIdCategoria" runat="server" />

            <asp:TextBox ID="txtnombreMod" runat="server" CssClass="input-modal" placeholder="Categoria" />
            <asp:TextBox ID="txtdescripcionMod" runat="server" CssClass="input-modal" placeholder="Descripcion" />
            <asp:DropDownList ID="ddlDescuentoMod" runat="server" CssClass="input-modal" />

            <%--<asp:TextBox ID="txtActivoMod" runat="server" CssClass="input-modal" placeholder="Activo" />--%>
        
            <asp:Button ID="btnActualizarCategoria" runat="server" Text="Actualizar" CssClass="btn-edit" OnClick="btnActualizarCategoria_Click" />
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
