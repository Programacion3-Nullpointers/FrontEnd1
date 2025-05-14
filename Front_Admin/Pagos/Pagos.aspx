<%@ Page Title="Pagos" Language="C#" MasterPageFile="~/MainLayou.master"
    AutoEventWireup="true" CodeFile="Pagos.aspx.cs"
    Inherits="Front_Admin.Pagos.Pagos" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            margin: 0;
            overflow-x: hidden;
            font-family: Arial, sans-serif;
        }

        .content {
            margin-left: 20px;
            padding: 20px;
            box-sizing: border-box;
        }

        .table-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 20px;
            flex-wrap: wrap;
            gap: 10px;
        }

        .table-header h2 {
            margin: 0;
            color: #2c3e50;
        }

        .btn-add {
            background-color: #27ae60;
            color: white;
            padding: 8px 16px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            text-decoration: none;
        }

        .btn-add:hover {
            background-color: #1e8449;
        }

        .select-modal {
            width: 150px;
            padding: 6px 10px;
            font-size: 14px;
            border-radius: 6px;
            border: 1px solid #ccc;
            cursor: pointer;
            appearance: none;
            background-color: #fff;
            background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='10' height='5'%3E%3Cpath fill='%23000' d='M0 0l5 5 5-5z'/%3E%3C/svg%3E");
            background-repeat: no-repeat;
            background-position: right 10px center;
            background-size: 10px 5px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #2c3e50;
            color: white;
        }

        .action-buttons button {
            border: none;
            padding: 6px 10px;
            margin: 0 4px;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-edit {
            background-color: #3498db;
            color: white;
        }

        .btn-edit:hover {
            background-color: #2980b9;
        }

        .btn-delete {
            background-color: #e74c3c;
            color: white;
        }

        .btn-delete:hover {
            background-color: #c0392b;
        }

        /* MODAL */
        .modal {
            display: none;
            position: fixed;
            z-index: 1000;
            top: 0; left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            box-sizing: border-box;
            overflow: auto;
            padding-left: 20px;
            padding-right: 20px;
        }

        .modal-content {
            background-color: white;
            margin: 80px auto;
            padding: 30px;
            width: 90%;
            max-width: 400px;
            border-radius: 12px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.3);
            position: relative;
            display: flex;
            flex-direction: column;
            gap: 12px;
            box-sizing: border-box;
        }

        .cerrar {
            position: absolute;
            right: 10px;
            top: 5px;
            color: #aaa;
            font-size: 24px;
            font-weight: bold;
            cursor: pointer;
        }

        .cerrar:hover {
            color: black;
        }

        .input-modal {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="content">
        <div class="table-header">
            <h2>Gestión de Pagos</h2>
            <asp:DropDownList ID="ddlFiltroTipo" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlFiltroTipo_SelectedIndexChanged" CssClass="input-modal select-modal">
                <asp:ListItem Text="Todos" Value="Todos" />
                <asp:ListItem Text="Boleta" Value="Boleta" />
                <asp:ListItem Text="Factura" Value="Factura" />
            </asp:DropDownList>
            <button type="button" class="btn-add" onclick="mostrarModal()">➕ Agregar Pago</button>
        </div>

        <asp:GridView ID="gvPagos" runat="server" AutoGenerateColumns="False" OnRowCommand="gvPagos_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                <asp:BoundField DataField="monto_total" HeaderText="Monto Total" DataFormatString="{0:C}" />
                <asp:BoundField DataField="fecha_pago" HeaderText="Fecha de Pago" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Detalle">
                    <ItemTemplate>
                        <asp:Literal ID="litDetalle" runat="server" Text='<%# Eval("detalle") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn-edit" Text="✏️" />
                        <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn-delete" Text="🗑️" OnClientClick="return confirm('¿Eliminar este pago?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Modal para Agregar Pago -->
    <asp:Panel ID="pnlModalAgregar" runat="server" CssClass="modal" Style="display: none;">
        <div class="modal-content">
            <span class="cerrar" onclick="cerrarModal()">&times;</span>
            <h3>Agregar Pago</h3>

            <asp:DropDownList ID="ddlTipoPago" runat="server" CssClass="input-modal">
                <asp:ListItem Text="Boleta" Value="Boleta" />
                <asp:ListItem Text="Factura" Value="Factura" />
            </asp:DropDownList>

            <asp:TextBox ID="txtFechaPago" runat="server" CssClass="input-modal" placeholder="Fecha de Pago (yyyy-mm-dd)" />
            <asp:TextBox ID="txtMonto" runat="server" CssClass="input-modal" placeholder="Monto Total" />

            <div><asp:TextBox ID="txtNombre" runat="server" CssClass="input-modal" placeholder="Nombre (Boleta)" /></div>
            <div><asp:TextBox ID="txtDNI" runat="server" CssClass="input-modal" placeholder="DNI (Boleta)" /></div>

            <div><asp:TextBox ID="txtRUC" runat="server" CssClass="input-modal" placeholder="RUC (Factura)" /></div>
            <div><asp:TextBox ID="txtRazonSocial" runat="server" CssClass="input-modal" placeholder="Razón Social (Factura)" /></div>
            <div><asp:TextBox ID="txtDireccion" runat="server" CssClass="input-modal" placeholder="Dirección (Factura)" /></div>

            <asp:TextBox ID="txtFechaEmision" runat="server" CssClass="input-modal" placeholder="Fecha Emisión (yyyy-mm-dd)" />

            <asp:Button ID="btnGuardarPago" runat="server" Text="Guardar" CssClass="btn-add" OnClick="btnGuardarPago_Click" />
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function mostrarModal() {
            document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'block';
            actualizarCamposPago();
        }

        function cerrarModal() {
            document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'none';
        }

        function actualizarCamposPago() {
            var tipo = document.getElementById('<%= ddlTipoPago.ClientID %>').value;

            var boletaCampos = ["txtNombre", "txtDNI"];
            var facturaCampos = ["txtRUC", "txtRazonSocial", "txtDireccion"];

            boletaCampos.forEach(id => {
                var elem = document.getElementById('<%= txtNombre.ClientID %>'.replace("txtNombre", id));
                if (elem) elem.parentElement.style.display = (tipo === "Boleta") ? "block" : "none";
            });

            facturaCampos.forEach(id => {
                var elem = document.getElementById('<%= txtRUC.ClientID %>'.replace("txtRUC", id));
                if (elem) elem.parentElement.style.display = (tipo === "Factura") ? "block" : "none";
            });
        }

        window.onload = function () {
            var ddl = document.getElementById('<%= ddlTipoPago.ClientID %>');
            if (ddl) {
                ddl.addEventListener("change", actualizarCamposPago);
            }
        }
    </script>
</asp:Content>
