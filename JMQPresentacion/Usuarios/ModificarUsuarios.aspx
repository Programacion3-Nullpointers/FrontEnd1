﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout2.Master" AutoEventWireup="true" CodeBehind="ModificarUsuarios.aspx.cs" Inherits="JMQPresentacion.Usuarios.ModificarUsuarios" %>
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
            <h2>Gestión de Usuarios</h2>
            <button type="button" class="btn-add" onclick="mostrarModal()">➕ Agregar Usuario</button>
        </div>

        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" OnRowCommand="gvUsuarios_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="correo" HeaderText="Correo" />
                <asp:BoundField DataField="razonsocial" HeaderText="Razón Social" />
                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="RUC" HeaderText="RUC" />

                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("id") %>' CssClass="btn-edit" Text="✏️" />
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
            <h3>Agregar Usuario</h3>

            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="input-modal" placeholder="Nombre de Usuario" />
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="input-modal" placeholder="Correo" />
            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="input-modal" placeholder="Razón Social" />
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="input-modal" placeholder="Dirección" />
            <asp:TextBox ID="txtRUC" runat="server" CssClass="input-modal" placeholder="RUC" />

            <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar" CssClass="btn-add" OnClick="btnGuardarUsuario_Click" />
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function mostrarModal() {
            document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'block';
        }

        function cerrarModal() {
            document.getElementById('<%= pnlModalAgregar.ClientID %>').style.display = 'none';
        }
    </script>
</asp:Content>