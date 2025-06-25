<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MainLayout2.Master" AutoEventWireup="true" CodeBehind="Descuentos.aspx.cs" Inherits="JMQPresentacion.Admin.Descuentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .dropdown-custom, .input-precio {
            width: 200px;
            padding: 8px 10px;
            border-radius: 6px;
            border: 1px solid #ccc;
            font-size: 14px;
            margin-right: 10px;
            margin-bottom: 20px;
        }

        .btn-filtrar-align {
            height: 38px;
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
            max-width: 600px;
        }

        .table-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
        }

        .btn-add {
            background-color: #28a745;
            color: white;
            border: none;
            padding: 8px 16px;
            border-radius: 6px;
            cursor: pointer;
        }

        .modal {
            position: fixed;
            top: 0; left: 0; width: 100%; height: 100%;
            background-color: rgba(0, 0, 0, 0.6);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
        }

        .modal-content {
            background-color: white;
            padding: 20px;
            border-radius: 10px;
            width: 400px;
            position: relative;
        }

        .cerrar {
            position: absolute;
            top: 10px; right: 10px;
            cursor: pointer;
            font-size: 20px;
        }

        .input-modal {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            border-radius: 6px;
            border: 1px solid #ccc;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Descuentos</h2>
            <button type="button" class="btn-add" onclick="mostrarModal()">➕ Agregar Descuento</button>
        </div>

    <asp:Button ID="btnResetFiltros" runat="server" Text="Lista Inicial" CssClass="btn btn-secondary btn-filtrar-align" OnClick="btnResetFiltros_Click" />

    <div class="filtros">
        <asp:DropDownList ID="ddlActivoFiltro" runat="server" CssClass="dropdown-custom">
            <asp:ListItem Text="Todos" Value="" />
            <asp:ListItem Text="Activos" Value="true" />
            <asp:ListItem Text="Inactivos" Value="false" />
        </asp:DropDownList>

        <asp:TextBox ID="txtPorcentajeMin" runat="server" CssClass="input-precio" placeholder="Min %" />
        <asp:TextBox ID="txtPorcentajeMax" runat="server" CssClass="input-precio" placeholder="Max %" />

        <asp:Button ID="btnAplicarFiltros" runat="server" Text="Aplicar" CssClass="btn btn-primary btn-filtrar-align" OnClick="btnAplicarFiltros_Click" />
    </div>

        <asp:GridView ID="gvDescuentos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvDescuentos_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="numDescuento" HeaderText="Descuento (%)" />
                <asp:TemplateField HeaderText="Activo">
                    <ItemTemplate>
                        <asp:Label ID="lblActivo" runat="server"
                            Text='<%# (Eval("activo").ToString().ToLower() == "true" || Eval("activo").ToString() == "1") ? "Sí" : "No" %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-primary btn-sm" Text="✏️"/>
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" Text="🗑️" OnClientClick="return confirm('¿Estás seguro que deseas eliminar este descuento?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


        <asp:Label ID="lblMensaje" runat="server" CssClass="alerta-producto" Visible="false" Text="⚠️ No se encontraron descuentos."></asp:Label>
    </div>

    <!-- Modal -->
    <asp:Panel ID="pnlModal" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModal()">&times;</span>
            <h3>Agregar/Editar Descuento</h3>

            <asp:HiddenField ID="hfIdDescuento" runat="server" />

            <asp:TextBox ID="txtNumDescuento" runat="server" CssClass="input-modal" placeholder="Porcentaje de descuento" />
            <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" CssClass="my-2" />
            <asp:Button ID="btnGuardarDescuento" runat="server" Text="Guardar" CssClass="btn btn-success w-100 mt-3" OnClick="btnGuardarDescuento_Click" />
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function mostrarModal() {
            document.getElementById('<%= pnlModal.ClientID %>').style.display = 'flex';
        }
        function cerrarModal() {
            document.getElementById('<%= pnlModal.ClientID %>').style.display = 'none';
        }
    </script>
</asp:Content>
